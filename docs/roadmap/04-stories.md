# 04 — Epics & User Stories

[← README](./README.md)

> Stories chi tiết với Acceptance Criteria, Business Rules, API và Security considerations.

---

## EPIC-AUTH: Authentication & Authorization

---

### AUTH-001: Customer Registration

```
ID: AUTH-001
Epic: EPIC-AUTH
Feature: Customer Authentication
User Story:
  As a: visitor
  I want: to create an account with email and password
  So that: I can make purchases and track my orders

Priority: P0 — Critical | Phase: MVP

Acceptance Criteria:
  1. User can submit: full name, email, phone number, password.
  2. Email must be unique — if already registered, return 409 Conflict.
  3. Password: min 8 chars, at least 1 uppercase and 1 number.
  4. Password is hashed (PBKDF2/bcrypt) before storage. Plain-text NEVER stored.
  5. On success, user is automatically logged in and redirected to homepage.
  6. Phone: optional, Vietnamese format if provided (10 digits, starts with 0).

Business Rules:
  - BR-AUTH-01: Email must be unique per customer.
  - BR-AUTH-02: Password stored with PBKDF2 / bcrypt.
  - BR-AUTH-03: Rate limit registration per IP (max 10/hour).

Dependencies: FOUND-001, FOUND-002

API:
  POST /api/auth/register
  Body: { fullName, email, phone?, password }
  Response 201: { accessToken, refreshToken, user: { id, fullName, email } }
  Response 409: { error: "Email already registered" }
  Response 422: Validation errors

Validation:
  - fullName: required, 2–100 chars
  - email: required, valid format, max 255 chars
  - password: required, 8–72 chars, complexity rules

Security: Never return password in response. HTTPS only.

QA:
  - Test duplicate email → 409
  - Test weak password → 422
  - Test password not stored in plain text (check DB)

Definition of Done:
  - Unit tests for validation and uniqueness check
  - Integration test for full registration flow
```

---

### AUTH-002: Customer Login

```
ID: AUTH-002
Epic: EPIC-AUTH | Priority: P0 — Critical | Phase: MVP

Acceptance Criteria:
  1. Customer submits email + password → receives JWT access token + refresh token.
  2. Access token expires in 15 minutes.
  3. Refresh token expires in 7 days.
  4. After 5 consecutive failed login attempts, account is locked for 15 minutes.
  5. Error message is generic: "Invalid email or password" (no user enumeration).
  6. Refresh token stored in HttpOnly, SameSite=Strict cookie.
  7. Remember Me option extends refresh token to 30 days.

Business Rules:
  - BR-AUTH-03: 5 failed attempts → 15-min lockout.
  - BR-AUTH-04: Generic error prevents user enumeration.

API:
  POST /api/auth/login
  POST /api/auth/refresh
  POST /api/auth/logout
```

---

### AUTH-003: Admin Login & RBAC

```
ID: AUTH-007/AUTH-008 | Priority: P0 — Critical | Phase: MVP

Acceptance Criteria:
  1. Admin logs in via /admin/login.
  2. Admin JWT contains role claims.
  3. API endpoints enforce role claims.
  4. Role "ProductManager" cannot access order endpoints.
  5. SuperAdmin has full access.

Roles:
  - SuperAdmin: Full access
  - ProductManager: Products, Catalog, Inventory, Pricing
  - OrderManager: Orders, Returns, Refunds
  - CustomerSupport: Read-only Customer/Orders; write Return/Refund
  - ContentManager: CMS, Blog, Banner
  - ReportViewer: Reports only (read-only)

Business Rules:
  - BR-AUTH-05: Admin accounts separate from customer accounts.
  - BR-AUTH-06: Admin session expires after 8h inactivity.
  - BR-AUTH-07: Admin login attempts logged to AuditLog.

Security:
  - RBAC enforced at API level — NOT just UI hiding.
```

---

## EPIC-PROD: Product Management

---

### PROD-001: Create Product

```
ID: PROD-001
Epic: EPIC-PROD | Priority: P0 — Critical | Phase: MVP

Acceptance Criteria:
  1. Admin fills in: Name, Slug (auto-generated, editable), Short/Full Description,
     Category, Brand, Status (Draft/Published), SEO metadata.
  2. Slug must be unique. System auto-generates from name; admin can override.
  3. Product created in "Draft" status by default.
  4. Admin must explicitly "Publish" for it to appear on client.
  5. Vietnamese slugification normalizes diacritics ("iPhone 15 Pro" → "iphone-15-pro").
  6. Required: Name, Category, Brand.

Business Rules:
  - BR-PROD-01: Product without variants cannot be purchased.
  - BR-PROD-02: Must have at least one published variant to be purchasable.
  - BR-PROD-03: Slug unique system-wide and URL-safe.
  - BR-PROD-04: Soft delete only if product has been in orders.

Dependencies: CAT-001, CAT-005

API:
  POST /api/admin/products
  Response 201: { id, name, slug, status }

Definition of Done:
  - Slug uniqueness enforced at DB level (unique index)
  - Published product appears on client listing
```

---

### PROD-005: Create Product Variant (SKU)

```
ID: PROD-005
Epic: EPIC-PROD | Priority: P0 — Critical | Phase: MVP

Acceptance Criteria:
  1. Admin can add multiple variants to a product.
  2. Each variant requires: SKU, Attribute combination, Price, Sale Price (optional), Stock Qty.
  3. SKU must be globally unique.
  4. Admin can set a variant as "Default".
  5. Variant can be individually activated/deactivated.
  6. If all variants inactive, product shows as unavailable.
  7. Sale price must be ≤ base price.

Business Rules:
  - BR-PROD-05: Each variant has a unique SKU.
  - BR-PROD-06: Sale price ≤ base price.
  - BR-PROD-07: stock = 0 → "Out of Stock", cannot add to cart.
  - BR-PROD-08: Variant with past orders: deactivate, do not delete.

Entity: ProductVariant { Id, ProductId, SKU, Attributes (JSON), BasePrice, SalePrice,
                         SortOrder, IsDefault, IsActive, RowVersion }

Technical Notes:
  - Optimistic concurrency on ProductVariant using RowVersion.
```

---

### PROD-009: Upload Product Images

```
ID: PROD-009
Epic: EPIC-PROD | Priority: P0 — Critical | Phase: MVP

Acceptance Criteria:
  1. Admin can upload multiple images per product (max 10).
  2. Images can be associated with specific variants.
  3. First image = "primary" shown in listing.
  4. Admin can drag-and-drop to reorder.
  5. Formats: JPG, PNG, WebP. Max 5MB per image.
  6. System generates thumbnails: 480px, 800px, 1200px.
  7. Images stored in object storage (not server disk).
  8. Admin can soft-delete images.

Business Rules:
  - BR-PROD-09: At least 1 image required before publishing.
  - BR-PROD-10: Image URLs served via CDN in production.

Technical Notes:
  - Object storage: AWS S3 / Azure Blob / MinIO for dev.
  - ImageSharp for resize/optimization.
```

---

## EPIC-INV: Inventory Management

---

### INV-003: Stock Reservation at Order Creation

```
ID: INV-003
Epic: EPIC-INV | Priority: P0 — Critical | Phase: MVP

Acceptance Criteria:
  1. Stock for each variant reserved immediately when order is created.
  2. If insufficient stock at creation (race condition): order fails with
     "Product X is no longer available in the requested quantity."
  3. Stock released fully if order cancelled before shipping.
  4. Reserved stock counted against available stock shown to customers.
  5. If Pending > 30 min without payment confirmation (online pay):
     reservation released and order cancelled.

Business Rules:
  - BR-INV-01: Available = Physical − Reserved.
  - BR-INV-02: Reservation is atomic (transaction + optimistic concurrency).
  - BR-INV-03: No overselling — only first of concurrent buyers succeeds.
  - BR-INV-04: Reservation expiry: COD=24h, Online=30min (configurable).

Technical Notes:
  - UPDATE Inventory SET ReservedQty += @qty
    WHERE Id = @id AND (PhysicalQty - ReservedQty) >= @qty
  - Background job: ReservationExpiryJob every 5 minutes.

Edge Cases:
  - 10 customers try to buy last unit → only 1 succeeds, 9 get clear error.
  - Customer abandons checkout → reservation expires after timeout.
```

---

## EPIC-CART: Cart

---

### CART-001: Add Product to Cart

```
ID: CART-001
Epic: EPIC-CART | Priority: P0 — Critical | Phase: MVP

Acceptance Criteria:
  1. Customer clicks "Add to Cart" → item added.
  2. If item already in cart, quantity increases by 1.
  3. Max quantity per item: min(10, available stock).
  4. Out-of-stock variant: button disabled, "Hết hàng" label.
  5. Unpublished product cannot be added.
  6. Cart badge in header updates in real-time.
  7. Guest cart: localStorage. Logged-in: server-side.
  8. On login: guest cart merged with server cart.

Business Rules:
  - BR-CART-01: Cannot add out-of-stock items.
  - BR-CART-02: Cannot add unpublished/deleted products.
  - BR-CART-03: PriceSnapshot at add-to-cart. Price change → notify on cart view.

API:
  POST /api/cart/items
  Body: { variantId, quantity }
  Response 200: Updated cart summary

Edge Cases:
  - Customer adds 5 units, stock drops to 3 before checkout → warn in cart.
  - Price increases after add to cart → "Price updated" banner.
```

---

## EPIC-ORDER: Order Management

---

### ORDER-003: Update Order Status (Admin)

```
ID: ORDER-003
Epic: EPIC-ORDER | Priority: P0 — Critical | Phase: MVP

Acceptance Criteria:
  1. Admin can transition status per valid state machine.
  2. Invalid transitions rejected with clear error.
  3. Each change recorded in OrderActivity log (timestamp + actor).
  4. Admin can add note to each transition.
  5. Customer notified by email on: Confirmed, Shipped, Delivered.

Valid Transitions:
  Pending → Confirmed
  Confirmed → Processing | Cancelled
  Processing → Shipped | Cancelled (requires note)
  Shipped → Delivered
  Delivered → Completed

Business Rules:
  - BR-ORDER-01: Cannot skip status levels.
  - BR-ORDER-02: Cancellation of paid order must trigger refund.
  - BR-ORDER-03: Post-Shipped → Return workflow only.
  - BR-ORDER-04: Stock reservation released on Cancelled.
  - BR-ORDER-05: Stock deducted (reservation → actual) on Shipped.
```

---

## EPIC-CHECKOUT: Checkout

---

### CHECKOUT-007: Place Order

```
ID: CHECKOUT-007
Epic: EPIC-CHECKOUT | Priority: P0 — Critical | Phase: MVP

Acceptance Criteria:
  1. Customer reviews summary (items, prices, delivery info, total).
  2. Customer accepts T&C checkbox.
  3. API performs final validation: stock, price, coupon, address.
  4. On validation fail: show specific error, preserve form data.
  5. On success: order created, confirmation page with order number.
  6. Order number: unique, human-readable (ORD-20260809-001234).
  7. Stock reserved atomically at order creation.
  8. Prices snapshotted into order line items.
  9. Coupon usage recorded at order creation.

Business Rules:
  - BR-ORDER-06: Price snapshot is immutable after creation.
  - BR-ORDER-07: Coupon re-validated server-side at order creation.
  - BR-ORDER-08: At least 1 item required.
  - BR-ORDER-09: Total calculated server-side only.

Technical Notes:
  - DB transaction: validate stock → reserve stock → create order
    → record coupon usage → commit.
  - On failure: full rollback.
  - OrderNumber: ORD-{YYYYMMDD}-{6-digit daily sequence}.

API:
  POST /api/orders
  Body: { cartId, deliveryAddress, deliveryMethod, paymentMethod, couponCode? }
  Response 201: { orderId, orderNumber, totalAmount }
  Response 422: Validation errors
  Response 409: Coupon already used

Edge Cases:
  - Stock runs out between cart validation and placement → fail with specific item.
  - Coupon expires between page load and submit → inform customer.
  - Network timeout → idempotency key prevents duplicate orders.
```

---

## EPIC-PROMO: Promotion & Coupon

---

### PROMO-001: Create Coupon (Admin)

```
ID: PROMO-001
Epic: EPIC-PROMO | Priority: P1 — High | Phase: MVP (basic) / Post-MVP (advanced)

Acceptance Criteria:
  1. Admin creates coupon with: Code, Type (Percentage / Fixed Amount), Value,
     Min Order Amount, Total Usage Limit, Per Customer Limit, Valid From/Until,
     Applicable Products/Categories (optional).
  2. Code is unique and case-insensitive.
  3. Admin can activate / deactivate coupon.
  4. Admin can view total usage count.
  5. Expired coupons automatically deactivated.

Business Rules:
  - BR-PROMO-01: Case-insensitive.
  - BR-PROMO-02: Percentage max 100%. Fixed max = order total.
  - BR-PROMO-03: Min order amount enforced.
  - BR-PROMO-04: Quota enforced atomically.
  - BR-PROMO-05: One coupon per order (MVP).
  - BR-PROMO-06: Discount cannot make total negative.

Edge Cases:
  - Two customers use last quota simultaneously → conditional UPDATE.
  - Order cancelled → usage count decremented.
```

---

## EPIC-SEARCH: Search & Discovery

---

### SEARCH-001: Product Keyword Search

```
ID: SEARCH-001
Epic: EPIC-SEARCH | Priority: P0 — Critical | Phase: MVP

Acceptance Criteria:
  1. Search bar in header on all pages.
  2. Customer types keyword → results at /tim-kiem?q=...
  3. Matches: product name, brand name, category name, SKU.
  4. Results show: image, name, price, rating average.
  5. Results filterable and sortable (same as PLP).
  6. Min 2 characters to trigger search.
  7. Case-insensitive, partial match supported.

Business Rules:
  - BR-SEARCH-01: Only published products in results.
  - BR-SEARCH-02: Out-of-stock shown but labeled.

Technical Notes (MVP):
  - SQL Server Full-Text Search CONTAINS/FREETEXT.
  - Index: Product.Name, Product.Description, Brand.Name
  - Post-MVP: Elasticsearch/Meilisearch for Vietnamese NLP.

API:
  GET /api/products/search?q=iphone&page=1&pageSize=20&brand=Apple&minPrice=5000000
```

---

## EPIC-REVIEW: Reviews & Ratings

---

### REVIEW-001: Submit Product Review

```
ID: REVIEW-001
Epic: EPIC-REVIEW | Priority: P2 — Medium | Phase: Post-MVP

Acceptance Criteria:
  1. Only verified purchasers can review (Delivered order containing the product).
  2. One review per customer per product.
  3. Form: Rating (1–5 stars, required), Title (optional, max 100), Body (optional, max 2000),
     Images (optional, max 5).
  4. Review defaults to "Pending Moderation".
  5. Pending reviews not shown on PDP until approved by admin.
  6. Customer sees own pending review with "Awaiting Moderation".
  7. Customer can edit while Pending.
  8. Average rating updated after review approval.

Business Rules:
  - BR-REVIEW-01: One review per customer per product (DB unique constraint).
  - BR-REVIEW-02: Verified purchasers only (server-side check).
  - BR-REVIEW-03: Moderation required before display.
  - BR-REVIEW-04: Rating average via denormalized column on Product.
```

---

## EPIC-RETURN: Return & Refund

---

### RETURN-001: Submit Return Request

```
ID: RETURN-001
Epic: EPIC-RETURN | Priority: P1 — High | Phase: Post-MVP

Acceptance Criteria:
  1. Customer submits return within 7 days of delivery (configurable).
  2. Form: select items, quantity, reason (Defective / Wrong / Not as Described / Changed Mind),
     description, photos (optional).
  3. Partial return supported (select specific items).
  4. After submission: status visible in Order Detail page.
  5. Admin notified of new request.

Business Rules:
  - BR-RETURN-01: Window = 7 days from delivery (configurable).
  - BR-RETURN-02: Non-returnable products cannot be returned.
  - BR-RETURN-03: Max return qty = original order qty.
  - BR-RETURN-04: Only for Delivered or Completed orders.

State Machine:
  Submitted → Under Review → Approved / Rejected
  Approved → Awaiting Item → Item Received → Refund Initiated → Refunded
```

---

### RETURN-004: Process Refund

```
ID: RETURN-004
Epic: EPIC-RETURN | Priority: P1 — High | Phase: Post-MVP

Acceptance Criteria:
  1. Admin approves return → triggers refund based on original payment method.
  2. COD: refund via bank transfer (manual).
  3. Online pay: refund via gateway API.
  4. Refund amount cannot exceed original payment.
  5. Partial refund supported.
  6. Refund status: Pending / Processing / Completed / Failed.
  7. Customer notified via email when refunded.

Business Rules:
  - BR-RETURN-05: Total refund ≤ total payment.
  - BR-RETURN-06: Refund attempt must be idempotent.
```
