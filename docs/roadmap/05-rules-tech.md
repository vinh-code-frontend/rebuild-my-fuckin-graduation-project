# 05 — Business Rules, Dependencies & Technical Considerations

[← README](./README.md)

---

## 10. Business Rules

### Product

| ID         | Rule                                                                  |
| ---------- | --------------------------------------------------------------------- |
| BR-PROD-01 | A product without active variants cannot be added to cart             |
| BR-PROD-02 | Product must be in Published status to appear on client               |
| BR-PROD-03 | Product slug must be unique and URL-safe                              |
| BR-PROD-04 | Products cannot be hard-deleted if they have associated orders        |
| BR-PROD-05 | Each product variant must have a globally unique SKU                  |
| BR-PROD-06 | Sale price must be ≤ base price                                       |
| BR-PROD-07 | Out-of-stock variants show "Out of Stock" and cannot be added to cart |
| BR-PROD-08 | Product images served via CDN, not direct server                      |
| BR-PROD-09 | Product must have at least 1 image to be published                    |

### Inventory

| ID        | Rule                                                                           |
| --------- | ------------------------------------------------------------------------------ |
| BR-INV-01 | Available Stock = Physical Stock − Reserved Stock                              |
| BR-INV-02 | Stock reservation must be atomic (transaction + optimistic concurrency)        |
| BR-INV-03 | No overselling — if two customers buy last unit, first wins, second gets error |
| BR-INV-04 | Reservation expiry is payment-method dependent (COD: 24h, Online: 30min)       |
| BR-INV-05 | Stock deduction occurs on order Shipped (reservation → actual deduction)       |
| BR-INV-06 | Reservation released on order Cancelled                                        |
| BR-INV-07 | Stock adjustment requires reason code and is logged to StockMovement           |

### Cart

| ID         | Rule                                                                   |
| ---------- | ---------------------------------------------------------------------- |
| BR-CART-01 | Cannot add out-of-stock items                                          |
| BR-CART-02 | Cannot add unpublished or deleted products                             |
| BR-CART-03 | Price snapshot at add-to-cart time; re-validate before order placement |
| BR-CART-04 | Guest cart merges with server cart on login                            |
| BR-CART-05 | Cart TTL: 30 days (logged-in), 7 days (guest)                          |
| BR-CART-06 | Max quantity per line item: min(10, available stock)                   |

### Order

| ID          | Rule                                                   |
| ----------- | ------------------------------------------------------ |
| BR-ORDER-01 | Order cannot skip status levels                        |
| BR-ORDER-02 | Paid order cancellation must trigger refund process    |
| BR-ORDER-03 | Post-Shipped cancellation goes through Return workflow |
| BR-ORDER-04 | Stock released when order cancelled                    |
| BR-ORDER-05 | Stock fully deducted when order shipped                |
| BR-ORDER-06 | Price snapshot is immutable after order creation       |
| BR-ORDER-07 | Order total calculated server-side only                |
| BR-ORDER-08 | Order must have at least 1 item                        |
| BR-ORDER-09 | Coupon re-validated server-side at order creation      |
| BR-ORDER-10 | Order number format: ORD-{YYYYMMDD}-{sequence}         |

### Payment

| ID        | Rule                                                                       |
| --------- | -------------------------------------------------------------------------- |
| BR-PAY-01 | Payment callbacks must validate signature/HMAC from gateway                |
| BR-PAY-02 | Payment processing must be idempotent — duplicate callbacks processed once |
| BR-PAY-03 | Raw card data must never be stored                                         |
| BR-PAY-04 | Payment amount must match order total                                      |
| BR-PAY-05 | Pending payment auto-cancels after timeout (configurable per method)       |

### Promotion

| ID          | Rule                                                             |
| ----------- | ---------------------------------------------------------------- |
| BR-PROMO-01 | Coupon code is case-insensitive                                  |
| BR-PROMO-02 | Percentage max: 100%. Fixed max: order total (no negative total) |
| BR-PROMO-03 | Minimum order amount respected before applying coupon            |
| BR-PROMO-04 | Coupon quota enforced atomically (no race condition)             |
| BR-PROMO-05 | One coupon per order (MVP)                                       |
| BR-PROMO-06 | Coupon re-validated server-side at order creation                |
| BR-PROMO-07 | Coupon usage incremented atomically at order creation            |
| BR-PROMO-08 | Coupon usage decremented if order cancelled before payment       |
| BR-PROMO-09 | Flash sale price is time-bounded and validated server-side       |

### Customer

| ID         | Rule                                                                |
| ---------- | ------------------------------------------------------------------- |
| BR-CUST-01 | Customer email is unique and immutable after registration           |
| BR-CUST-02 | Customer account can be deactivated but not deleted if orders exist |
| BR-CUST-03 | Password reset token expires in 1 hour                              |
| BR-CUST-04 | Customers cannot see other customers' data                          |

### Review

| ID           | Rule                                      |
| ------------ | ----------------------------------------- |
| BR-REVIEW-01 | One review per customer per product       |
| BR-REVIEW-02 | Only verified purchasers can review       |
| BR-REVIEW-03 | Reviews require moderation before display |

### Return

| ID           | Rule                                               |
| ------------ | -------------------------------------------------- |
| BR-RETURN-01 | Return window: 7 days from delivery (configurable) |
| BR-RETURN-02 | Non-returnable products cannot be returned         |
| BR-RETURN-03 | Return quantity ≤ purchased quantity               |
| BR-RETURN-04 | Total refund ≤ total payment                       |
| BR-RETURN-05 | Refund process must be idempotent                  |

---

## 11. Dependencies Map

```
FOUND-001 (DB Schema)
    ↓
CAT-001 (Categories) ──────────── CAT-005 (Brands)
    ↓                                   ↓
PROD-001 (Create Product) ──────────────┘
    ↓
PROD-005 (Variants/SKU)
    ↓
PROD-009 (Images) ──── INV-001 (Stock)
    ↓                       ↓
PLP-001 (Product Listing)   INV-003 (Reservation)
    ↓                           ↓
PDP-001 (Product Detail)    CART-001 (Add to Cart)
    ↓                           ↓
SEARCH-001 (Search)         CHECKOUT-007 (Place Order)
                                ↓
AUTH-001 (Register) ──── ORDER-001 (Order Created)
AUTH-002 (Login)             ↓              ↓
    ↓                  PAY-001 (COD)   PAY-004 (Online Pay)
USER-003 (Address) ──────────┘
    ↓
ORDER-003 (Status Update)
    ↓
ORDER-005 (Cancel) ──── RETURN-001 (Return Request)
                                ↓
                        RETURN-004 (Refund)
                                ↓
PAY-004 → PAY-006 (Gateway Callback)

PROMO-001 (Coupon) → PROMO-004 (Apply at Checkout)
                            ↓
                    CHECKOUT-007 (Order with Coupon)
```

---

## 12. Technical Considerations

### Database Schema (Core Entities)

```sql
Customer       { Id, Email, PhoneNumber, FullName, PasswordHash, IsActive, CreatedAt, UpdatedAt }
AdminUser      { Id, Email, PasswordHash, RoleId, IsActive, CreatedAt, LastLoginAt }
Role           { Id, Name, Permissions (JSON) }

Category       { Id, ParentId, Name, Slug, SortOrder, IsActive }
Brand          { Id, Name, Slug, LogoUrl, IsActive }
Attribute      { Id, Name, IsFilterable, Type }
AttributeValue { Id, AttributeId, Value }

Product        { Id, Name, Slug, Description, CategoryId, BrandId, Status, SEO (JSON),
                 CreatedAt, UpdatedAt, IsDeleted, RowVersion }
ProductVariant { Id, ProductId, SKU, AttributeValues (JSON), BasePrice, SalePrice,
                 SortOrder, IsDefault, IsActive, RowVersion }
ProductImage   { Id, ProductId, VariantId?, Url, ThumbnailUrl, SortOrder, IsDeleted }
ProductSpec    { Id, ProductId, AttributeId, Value }

Inventory      { Id, VariantId, PhysicalQty, ReservedQty, RowVersion }
               -- Available = PhysicalQty - ReservedQty
StockMovement  { Id, VariantId, ChangeType, Delta, Reason, OrderId?, ChangedBy, ChangedAt }
StockReservation { Id, VariantId, OrderId, Quantity, ReservedAt, ExpiresAt, IsReleased }

Cart           { Id, CustomerId?, SessionId, UpdatedAt, ExpiresAt }
CartItem       { Id, CartId, VariantId, Quantity, PriceSnapshot, AddedAt }

Order          { Id, OrderNumber, CustomerId?, Status, PaymentStatus,
                 ShippingAddress (JSON), SubTotal, ShippingFee, DiscountAmount, Total,
                 CouponCode, CouponSnapshot (JSON), CreatedAt, RowVersion }
OrderItem      { Id, OrderId, VariantId, SKU, ProductName, VariantLabel,
                 ImageUrl, UnitPrice, Quantity, LineTotal }
OrderActivity  { Id, OrderId, FromStatus, ToStatus, Note, ChangedBy, ChangedAt }

Payment        { Id, OrderId, Method, Status, Amount, GatewayTransactionId,
                 IdempotencyKey, ProcessedAt }

Coupon         { Id, Code, Type, Value, MinOrderAmount, TotalUsageLimit,
                 PerCustomerLimit, ValidFrom, ValidUntil, UsageCount, IsActive }
CouponUsage    { Id, CouponId, CustomerId, OrderId, UsedAt }

ReturnRequest  { Id, OrderId, CustomerId, Status, Reason, Description, CreatedAt }
ReturnItem     { Id, ReturnRequestId, OrderItemId, Quantity, Condition }
Refund         { Id, ReturnRequestId, PaymentId, Amount, Status, ProcessedAt }
```

### Indexing Strategy

| Table            | Index                                                                         |
| ---------------- | ----------------------------------------------------------------------------- |
| Product          | `(Slug) UNIQUE`, `(Status, IsDeleted)`, `(CategoryId, Status)`, `(BrandId)`   |
| ProductVariant   | `(SKU) UNIQUE`, `(ProductId)`                                                 |
| Order            | `(OrderNumber) UNIQUE`, `(CustomerId, CreatedAt DESC)`, `(Status, CreatedAt)` |
| CartItem         | `(CartId, VariantId) UNIQUE`                                                  |
| Coupon           | `(Code) UNIQUE NONCLUSTERED`                                                  |
| CouponUsage      | `(CouponId, CustomerId) UNIQUE`                                               |
| StockReservation | `(VariantId, IsReleased, ExpiresAt)`                                          |

### Concurrency Handling

| Scenario                    | Strategy                                                                                                                   |
| --------------------------- | -------------------------------------------------------------------------------------------------------------------------- |
| Two customers buy last unit | `UPDATE Inventory SET ReservedQty += @qty WHERE Id=@id AND (PhysicalQty - ReservedQty) >= @qty` → if 0 rows affected, fail |
| Coupon last quota race      | `UPDATE Coupon SET UsageCount += 1 WHERE Id=@id AND UsageCount < TotalUsageLimit` → if 0 rows affected, fail               |
| Admin updates product/price | RowVersion on ProductVariant — return 409 Conflict if outdated                                                             |
| Payment double-callback     | IdempotencyKey on Payment — ignore if already in Success state                                                             |

### Transactions

- **Order creation:** validate stock → reserve stock → create order + items → record coupon usage → commit.
- **Refund processing:** wrap refund record creation + payment status update.
- **Order cancellation:** wrap status update + stock release.

### Background Jobs

| Job                        | Trigger            | Action                                                         |
| -------------------------- | ------------------ | -------------------------------------------------------------- |
| `ReservationExpiryJob`     | Every 5 min        | Release expired reservations, cancel associated pending orders |
| `PendingPaymentTimeoutJob` | Every 5 min        | Cancel orders with expired pending online payments             |
| `RatingAggregationJob`     | On review approval | Update `Product.AverageRating`, `Product.ReviewCount`          |
| `LowStockAlertJob`         | Every hour         | Notify admins of SKUs below threshold                          |

### Caching Strategy (Post-MVP)

| Data             | Strategy          | TTL                          |
| ---------------- | ----------------- | ---------------------------- |
| Product detail   | Redis / In-memory | 5 min (invalidate on update) |
| Category tree    | Redis             | 30 min                       |
| Homepage banners | Redis             | 10 min                       |
| Search results   | Redis             | 2 min                        |
| Cart             | Server-side (DB)  | N/A                          |

---

## 13. Security Requirements

### Authentication

| Requirement      | Detail                                             |
| ---------------- | -------------------------------------------------- |
| JWT Access Token | Signed RS256 or HS256, 15-minute expiry            |
| Refresh Token    | Rotated on each use, HttpOnly cookie, 7-day expiry |
| Password Hashing | PBKDF2 (min 100k iterations) or bcrypt (cost 12)   |
| Account Lockout  | 5 failed attempts → 15-min lockout                 |
| Admin Session    | 8-hour inactivity timeout                          |
| Password Reset   | Single-use token, 1-hour expiry, HTTPS-only        |

### Authorization

| Requirement               | Detail                                                           |
| ------------------------- | ---------------------------------------------------------------- |
| RBAC                      | All admin endpoints enforce role claims in JWT                   |
| Resource Ownership        | Customers can only access their own orders, cart, profile        |
| Admin/Customer Separation | Separate token issuers or audience claims                        |
| API Route Protection      | Auth middleware global; explicit allow-list for public endpoints |

### Input Security

| Requirement     | Detail                                                                   |
| --------------- | ------------------------------------------------------------------------ |
| SQL Injection   | EF Core parameterized queries — no raw SQL with user input               |
| XSS             | HTML-escape all user-generated content; Content-Security-Policy header   |
| CSRF            | SameSite=Strict cookie for refresh token; stateless JWT for access token |
| Rate Limiting   | Auth: 10 req/min/IP. Search: 60 req/min. General: 100 req/min            |
| File Upload     | Validate MIME type server-side; max size enforced; store outside webroot |
| Mass Assignment | Use DTOs — never bind entity directly from request body                  |

### Admin Portal Security

| Requirement              | Detail                                                         |
| ------------------------ | -------------------------------------------------------------- |
| Audit Log                | All admin write actions logged: who, what, when, old/new value |
| Permission Check         | RBAC enforced at API level — UI hiding alone is insufficient   |
| Admin Account Management | Deactivated by SuperAdmin                                      |
| Session Revocation       | Refresh token revocation supported (revoked flag in DB)        |
