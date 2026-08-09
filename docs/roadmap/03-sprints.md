# 03 — Sprint Planning

[← README](./README.md)

> Chi tiết từng sprint: goal, stories, deliverables, technical notes, risks.

---

## Sprint 0 — Foundation & Infrastructure

**Sprint Goal:** Thiết lập toàn bộ foundation: project structure, database schema cơ bản, CI/CD pipeline, coding conventions và shared infrastructure.

**Business Objective:** Không có deliverable cho user, nhưng đây là nền tảng để toàn bộ sprint sau có thể chạy song song và hiệu quả.

**Stories:**

- FOUND-001: Database schema design (core entities)
- FOUND-002: EF Core setup, migrations scaffold
- FOUND-003: Global exception handling middleware
- FOUND-004: Logging infrastructure (Serilog)
- FOUND-005: Swagger / OpenAPI setup
- FOUND-006: CORS configuration
- FOUND-007: Nuxt project bootstrap (layouts, composables, API layer)
- FOUND-008: React Admin bootstrap (layout, routing, auth guard)
- FOUND-009: Environment configuration (.env, appsettings per environment)
- FOUND-010: CI pipeline (build, test, lint)

**Dependencies:** None

**Expected Deliverables:**

- Running API with health check endpoint
- DB schema with initial migration
- Nuxt & React Admin scaffold with layout
- CI pipeline passing

**Demo Scenario:** Health check endpoint returns 200. DB migrations run without error.

**Technical Notes:**

- Use `rowversion` / `timestamp` column on entities for optimistic concurrency from the start.
- Add `CreatedAt`, `UpdatedAt`, `IsDeleted` to all major entities (soft delete).
- Establish coding conventions: naming, DTO patterns, error response format.

---

## Sprint 1 — Authentication & RBAC

**Sprint Goal:** Khách hàng có thể đăng ký, đăng nhập, quản lý session. Admin có thể login và được phân quyền theo role.

**Business Objective:** Bảo mật hệ thống, tạo nền tảng cho các tính năng yêu cầu authentication.

**Stories:**

- AUTH-001: Customer Register
- AUTH-002: Customer Login
- AUTH-003: Customer Logout
- AUTH-004: Forgot Password
- AUTH-005: Reset Password
- AUTH-006: JWT + Refresh Token
- AUTH-007: Admin Login
- AUTH-008: Admin Role Management (RBAC)
- AUTH-009: Admin Permission Assignment
- USER-001: View Customer Profile (stub)
- USER-002: Update Customer Profile
- USER-010: Change Password
- PERM-001: Create Admin User
- PERM-002: Manage Roles & Permissions
- NOTIFY-003: Password Reset Email

**Expected Deliverables:**

- Customer can register & login on Nuxt
- Admin can login on React Admin
- JWT + Refresh Token working
- Role-based middleware protecting admin endpoints

**Demo Scenario:** Customer registers → logs in → sees protected profile page. Admin logs in with role "ProductManager" → cannot access Order Management.

**Risks:** Refresh token rotation logic needs careful implementation to prevent token theft.

---

## Sprint 2 — Catalog (Categories, Brands, Attributes)

**Sprint Goal:** Admin có thể quản lý danh mục, thương hiệu và thuộc tính sản phẩm. Client hiển thị category navigation.

**Business Objective:** Catalog structure là prerequisite cho toàn bộ product features.

**Stories:**

- CAT-001: Create Category
- CAT-002: Update Category
- CAT-003: Delete Category (Soft)
- CAT-004: Category Hierarchy (tree structure)
- CAT-005: Create Brand
- CAT-006: Update Brand
- CAT-007: Delete Brand (Soft)
- CAT-008: Manage Product Attributes (e.g., "RAM", "Storage", "Color")
- CAT-009: Manage Attribute Values (e.g., "8GB", "256GB", "Black")
- CAT-010: Category Navigation on Client (menu)

**Expected Deliverables:**

- Admin can create category tree (e.g., Điện thoại > iPhone > iPhone 15)
- Admin can create brands (Apple, Samsung, Xiaomi)
- Category navigation visible on Nuxt client

**Technical Notes:**

- Category hierarchy: use `ParentCategoryId` self-referencing FK. Limit to 3 levels for MVP.
- Brand entity: `Id`, `Name`, `Slug`, `LogoUrl`, `IsActive`
- Attribute: `Id`, `Name`, `Type (text/number/select)`, `IsFilterable`

---

## Sprint 3 — Product Management (Admin) + Product Listing (Client)

**Sprint Goal:** Admin có thể tạo, cập nhật, publish sản phẩm. Client hiển thị danh sách sản phẩm theo category.

**Business Objective:** Sản phẩm là core asset của platform. Không có sản phẩm thì không có doanh thu.

**Stories:**

- PROD-001: Create Product
- PROD-002: Update Product
- PROD-003: Soft Delete Product
- PROD-004: Publish / Unpublish Product
- PROD-005: Product List (Admin) with search/filter
- PROD-006: Manage Product SEO (slug, meta title, meta description)
- PLP-001: Product Listing Page (Client)
- PLP-002: Category Filter on PLP
- PLP-003: Brand Filter on PLP
- PLP-004: Sort by Price / Newest / Best Seller
- PLP-005: Pagination on PLP
- HOME-001: Hero Banner Display (static)
- HOME-002: Featured Products Section

**Expected Deliverables:**

- Admin creates "iPhone 15" product, publishes it
- Customer sees it on `/dien-thoai/iphone` listing page
- Pagination working

**Technical Notes:**

- `Product` entity: `Id`, `Name`, `Slug`, `Description`, `CategoryId`, `BrandId`, `Status (Draft/Published/Archived)`, `CreatedAt`, `UpdatedAt`, `IsDeleted`
- Slug must be unique; auto-generate from name with Vietnamese slug normalization.

---

## Sprint 4 — Product Detail, Variants & Images

**Sprint Goal:** Admin có thể tạo variants (SKU) và upload ảnh. Client hiển thị Product Detail Page đầy đủ.

**Business Objective:** Product detail quality trực tiếp ảnh hưởng đến conversion rate.

**Stories:**

- PROD-007: Create Product Variant (SKU)
- PROD-008: Update Product Variant
- PROD-009: Upload Product Images
- PROD-010: Manage Product Specifications
- PROD-011: Set Variant-specific Price & Images
- PDP-001: Product Detail Page (Client)
- PDP-002: Product Image Gallery (thumbnails + zoom)
- PDP-003: Variant Selection (Color, Storage)
- PDP-004: Price Display (base + sale price)
- PDP-005: Stock Availability Indicator
- PDP-006: Product Specifications Table
- PDP-007: Breadcrumb Navigation
- PDP-008: Related Products (same category)
- PERF-002: CDN integration for images
- PERF-003: Image compression (WebP + srcset)
- PERF-004: Lazy loading images

**Expected Deliverables:**

- Admin creates iPhone 15 with variants (Black 128GB, Black 256GB, etc.)
- Client shows PDP with image gallery, variant switcher, correct price per variant
- Out-of-stock variant shown as unavailable

**Technical Notes:**

- `ProductVariant` entity: `Id`, `ProductId`, `SKU`, `Attributes (JSON or AttributeValues)`, `Price`, `SalePrice`, `StockQuantity`, `IsActive`
- SKU must be unique system-wide.
- Images: `ProductImage` entity linked to `ProductId` and optionally `VariantId`. Ordered by `SortOrder`.
- Consider object storage for images (required) — local disk not suitable for production.

---

## Sprint 5 — Search & Discovery

**Sprint Goal:** Khách hàng có thể tìm kiếm sản phẩm bằng từ khóa, với autocomplete và filter.

**Business Objective:** Search là top customer acquisition channel sau category browse.

**Stories:**

- SEARCH-001: Keyword Search API
- SEARCH-002: Autocomplete Suggestions
- SEARCH-003: Search Results Page (Client)
- SEARCH-004: Filter on Search Results (brand, price, availability)
- SEARCH-005: Sort on Search Results
- SEARCH-007: Empty Search Result State
- PLP-006: Price Range Filter
- PLP-007: Availability Filter

**Expected Deliverables:**

- Customer types "iphone 15" → sees relevant results
- Autocomplete shows suggestions after 2+ chars
- Empty state shows popular products

**Technical Notes:**

- MVP: Full-text search via SQL Server `CONTAINS` / `FREETEXT`. Index `Product.Name`, `Product.Description`.
- Post-MVP: Migrate to Elasticsearch or Meilisearch for Vietnamese language support, typo tolerance, relevance ranking.
- Vietnamese typo tolerance (MVP): normalize diacritics when indexing.

---

## Sprint 6 — Cart

**Sprint Goal:** Khách hàng có thể thêm sản phẩm vào giỏ, cập nhật số lượng, xóa và xem tổng giá.

**Business Objective:** Cart is the primary conversion driver. Errors here directly kill revenue.

**Stories:**

- CART-001: Add Product to Cart
- CART-002: Update Cart Item Quantity
- CART-003: Remove Cart Item
- CART-004: View Cart Summary
- CART-005: Cart Persistence (logged-in user: server-side; guest: localStorage)
- CART-006: Stock Validation when Adding to Cart
- CART-007: Price Re-validation on Cart View
- CART-008: Merge Guest Cart on Login

**Expected Deliverables:**

- Customer adds iPhone 15 Black 128GB to cart
- Quantity update updates subtotal in real-time
- If SKU becomes out-of-stock while in cart, show warning
- Cart persists after browser refresh

**Technical Notes:**

- `Cart` entity: belongs to `CustomerId` (or anonymous session).
- `CartItem`: `CartId`, `ProductVariantId`, `Quantity`, `PriceSnapshot` (captured at add-to-cart time), `UpdatedAt`
- Do NOT reserve inventory at cart stage — only reserve at order creation.
- Cart TTL: 30 days for logged-in, 7 days for guest (configurable).

**Edge Cases:**

- Product unpublished while in cart → show warning, disable checkout for that item.
- Price changes while in cart → re-validate and show price changed banner.
- Quantity requested > stock → cap at max available and notify.

---

## Sprint 7 — Checkout & Address

**Sprint Goal:** Khách hàng có thể hoàn tất checkout: nhập thông tin, chọn địa chỉ giao hàng, xem order summary và đặt hàng.

**Business Objective:** Checkout là điểm quan trọng nhất trong conversion funnel.

**Stories:**

- CHECKOUT-001: Checkout Page — Customer Info Form
- CHECKOUT-002: Select Saved Address
- CHECKOUT-003: Add New Address at Checkout
- CHECKOUT-004: Select Delivery Method (Standard / Express)
- CHECKOUT-005: Shipping Fee Calculation
- CHECKOUT-006: Order Summary (items, subtotal, shipping, total)
- CHECKOUT-007: Place Order (COD)
- CHECKOUT-008: Order Confirmation Page
- USER-003: Manage Saved Addresses
- USER-004: Add / Edit / Delete Address
- USER-005: Set Default Address

**Expected Deliverables:**

- End-to-end checkout: Cart → Checkout → Order placed → Confirmation page
- Guest checkout supported (no login required for MVP)
- Address form with validation (required fields, phone format)

**Technical Notes:**

- Snapshot product name, SKU, price, image at order creation time — do NOT use live product references in order line items.
- Validate stock once more at order creation (between cart validation and order placement).
- Use DB transaction wrapping: stock reservation + order creation must be atomic.
- `Order.ShippingAddressSnapshot` as JSON field (snapshot, not FK to address table).

---

## Sprint 8 — Order Management

**Sprint Goal:** Admin có thể xem và quản lý đơn hàng. Khách hàng có thể xem lịch sử và chi tiết đơn hàng.

**Business Objective:** Order management là operational backbone. Thiếu tính năng này admin không thể xử lý đơn.

**Stories:**

- ORDER-001: Order List (Admin) — with filter by status, date, customer
- ORDER-002: Order Detail (Admin)
- ORDER-003: Update Order Status (Admin)
- ORDER-004: Order Timeline / Activity Log
- ORDER-005: Cancel Order (Admin)
- ORDER-006: Customer Order History Page
- ORDER-007: Customer Order Detail Page
- ORDER-008: Order Status Tracking (Customer)
- ORDER-009: Cancel Order (Customer — before processing)
- USER-006: Customer List (Admin)
- USER-007: Customer Detail (Admin)
- USER-008: Customer Order History (Admin View)

**Expected Deliverables:**

- Admin sees all orders, filters by "Pending", updates to "Processing", then "Shipped"
- Customer sees order history, clicks into detail, sees timeline
- Customer can cancel Pending order

**Technical Notes:**

- Order Status State Machine:
  ```
  Pending → Confirmed → Processing → Shipped → Delivered → Completed
       ↓           ↓          ↓
  Cancelled    Cancelled   Cancelled (requires refund if paid)
  ```
- Enforce valid state transitions at service layer.
- `OrderActivity` table: `OrderId`, `FromStatus`, `ToStatus`, `ChangedBy`, `ChangedAt`, `Note`

---

## Sprint 9 — Inventory Management + Payment (COD)

**Sprint Goal:** Admin có thể quản lý tồn kho. COD payment hoạt động end-to-end.

**Business Objective:** Inventory accuracy prevents overselling. COD is the primary payment method for MVP.

**Stories:**

- INV-001: View Stock Level per SKU (Admin)
- INV-002: Manual Stock Adjustment (Admin)
- INV-003: Stock Reservation at Order Creation
- INV-004: Release Reservation on Order Cancel
- INV-005: Release Reservation on Order Complete
- INV-006: Low Stock Alert (Admin notification)
- INV-007: Stock Movement History
- PAY-001: Cash on Delivery Payment Flow
- PAY-002: Mark Order as Paid (Admin — for COD verification)
- PAY-003: Payment Status Tracking
- NOTIFY-001: Order Confirmation Email
- NOTIFY-002: Order Status Update Email

**Expected Deliverables:**

- Admin adjusts stock for iPhone 15 SKU from 50 → 45
- Customer places order → stock reserved → admin confirms → shipped → stock released
- Low stock alert triggers when stock < threshold

**Technical Notes:**

- Use optimistic concurrency (rowversion) when updating `StockQuantity`.
- Reservation: `StockReservation` table: `VariantId`, `OrderId`, `Quantity`, `ReservedAt`, `ExpiresAt`
- Reservation expiry job: release reservation if order not confirmed within X minutes (configurable).
- For MVP: single warehouse, single inventory pool per SKU.

---

## Sprint 10 — Promotion & Coupon

**Sprint Goal:** Admin có thể tạo coupon. Khách hàng có thể áp dụng coupon tại checkout.

**Business Objective:** Promotion drives acquisition and repeat purchase. Required for marketing campaigns.

**Stories:**

- PROMO-001: Create Coupon (Admin)
- PROMO-002: Update / Deactivate Coupon (Admin)
- PROMO-003: Coupon List (Admin)
- PROMO-004: Apply Coupon at Checkout (Client)
- PROMO-005: Coupon Validation (server-side)
- PROMO-006: Product-level Discount (Admin sets sale price)
- PROMO-007: Flash Sale (time-limited discount)
- PROMO-008: Flash Sale Display on Client
- PROMO-009: Promotion Applied in Order Summary
- PROMO-010: Coupon Usage Tracking
- PROMO-012: Free Shipping Promotion
- PROMO-013: Campaign Management
- HOME-003: Flash Sale Section on Homepage

**Expected Deliverables:**

- Admin creates coupon "WELCOME10" (10% off, max 1 use per customer, expires 30 days)
- Customer enters "WELCOME10" at checkout → 10% applied → order created with discount snapshot
- Coupon cannot be used after expiry or quota exhausted

**Technical Notes:**

- Always re-validate coupon server-side at order creation — never trust client-side discount.
- Snapshot discount amount and coupon code into `Order` entity.
- `CouponUsage` table: `CouponId`, `CustomerId`, `OrderId`, `UsedAt` — enforce unique-per-customer if applicable.
- Concurrency: use database-level check (SELECT + UPDATE in transaction) to prevent race condition on quota.

---

## Sprint 11 — Online Payment Gateway

**Sprint Goal:** Tích hợp payment gateway cho phép thanh toán online qua thẻ và e-wallet.

**Business Objective:** Online payment increases conversion for customers who don't want COD. Reduces failed delivery risk.

**Stories:**

- PAY-004: Integrate Payment Gateway (VNPAY / MoMo / ZaloPay)
- PAY-005: Payment Redirect Flow
- PAY-006: Payment Callback / Webhook Handling
- PAY-007: Payment Idempotency
- PAY-008: Payment Retry Logic
- PAY-009: Pending Payment Timeout Handling
- PAY-010: Payment Failed Handling
- PAY-011: Payment Success → Order Confirm Flow

**Expected Deliverables:**

- Customer chooses "Pay with VNPAY" → redirected → pays → returned to confirmation page
- If callback received twice, only process once (idempotency)
- If payment times out, order returns to Pending/Cancelled

**Technical Notes:**

- Never store raw card data. Use tokenization provided by gateway.
- Webhook endpoint must validate signature/HMAC from gateway before processing.
- `Payment` entity: `Id`, `OrderId`, `Method`, `Status (Pending/Success/Failed/Cancelled)`, `GatewayTransactionId`, `Amount`, `ProcessedAt`, `IdempotencyKey`
- Implement `PaymentIdempotencyKey` = `OrderId + Attempt` to prevent double-charge.
- Background job to poll payment status for stuck Pending payments.

**Edge Cases:**

- Payment success callback arrives but network timeout returns error to frontend → customer sees failure but payment was successful.
- Solution: reconciliation job checks gateway status for all Pending payments every 5 minutes.

---

## Sprint 12 — Customer Account Features

**Sprint Goal:** Bổ sung tính năng tài khoản: wishlist, recently viewed, notifications.

**Stories:**

- WISH-001: Add to Wishlist
- WISH-002: View Wishlist
- WISH-003: Remove from Wishlist
- WISH-004: Move Wishlist Item to Cart
- REC-001: Recently Viewed Products (tracked client-side + server for logged-in)
- NOTIFY-004: Low Stock Alert Email (Admin)
- NOTIFY-005: SMS OTP / Order Notification
- USER-009: Account Dashboard Page
- ORDER-010: Re-order (add items to cart)
- SEARCH-006: Search History
- HOME-007: Recently Viewed on Homepage

**Technical Notes:**

- Email: Use transactional email provider (SendGrid / AWS SES / SMTP). Template-based HTML emails.
- Recently viewed: store last 20 items. Client-side for guest, server-side for logged-in.

---

## Sprint 13 — Reviews, Q&A & Product Comparison

**Sprint Goal:** Khách hàng có thể đánh giá sản phẩm, đặt câu hỏi và so sánh các sản phẩm.

**Stories:**

- REVIEW-001: Submit Product Review (rating + text)
- REVIEW-002: Upload Review Images
- REVIEW-003: View Product Reviews on PDP
- REVIEW-004: Review Moderation (Admin — approve/reject)
- REVIEW-005: Verified Purchase Badge
- REVIEW-006: Review Helpfulness (upvote)
- QA-001: Ask Product Question (Client)
- QA-002: Answer Question (Admin or other customers)
- QA-003: Q&A Display on PDP
- COMPARE-001: Add Product to Compare List
- COMPARE-002: Product Comparison Page
- COMPARE-003: Highlight Specification Differences
- PLP-008: Rating Filter on PLP
- PDP-012: Accessories Section
- SEARCH-008: Search Result Keyword Highlight

**Technical Notes:**

- Review can only be submitted by verified purchasers (check `Order` history for `ProductId`).
- Rating average updated via background job or denormalized on `Product` entity.
- Compare: max 4 products, stored in localStorage.

---

## Sprint 14 — Return, Refund & Warranty

**Sprint Goal:** Khách hàng có thể gửi yêu cầu trả hàng/hoàn tiền. Admin có thể xử lý workflow.

**Stories:**

- RETURN-001: Submit Return Request (Customer)
- RETURN-002: Return Request List (Admin)
- RETURN-003: Approve / Reject Return (Admin)
- RETURN-004: Process Refund (Admin)
- RETURN-005: Refund to Original Payment Method
- RETURN-006: Return Status Tracking (Customer)
- WARRANTY-001: Warranty Information on PDP
- WARRANTY-002: Warranty Claim Request (Customer)
- WARRANTY-003: Warranty Claim Management (Admin)
- PDP-010: Warranty Information Display on PDP
- PDP-009: Installment Information Display

**Technical Notes:**

- Return window: configurable (default 7 days from delivery).
- Partial return supported (item-level, not order-level).
- Refund amount cannot exceed original payment amount.
- `ReturnRequest` state machine: `Submitted → Under Review → Approved / Rejected → Refunded`

---

## Sprint 15 — Content Management & SEO Optimization

**Sprint Goal:** Admin có thể quản lý homepage content. Nuxt client đạt chuẩn SEO.

**Stories:**

- CMS-001: Homepage Banner Management (Admin)
- CMS-002: Navigation Menu Configuration
- CMS-003: SEO Metadata per Page
- CMS-004: Sitemap Generation (`/sitemap.xml`)
- CMS-005: Robots.txt Configuration
- CMS-006: Structured Data (Product Schema, Breadcrumb)
- CMS-007: Open Graph Tags
- CMS-008: Blog/News CRUD (Admin)
- CMS-009: Blog List + Detail Page (Client)
- CMS-010: FAQ Management (Admin) + FAQ Page (Client)
- PDP-011: Product Video Support
- HOME-008: Brand Showcase Section

**Technical Notes:**

- Use `useHead()` in Nuxt for dynamic meta tags.
- Product schema: `@type: Product`, price, availability, review rating.
- Breadcrumb schema on PLP and PDP.
- Canonical URL on all pages to prevent duplicate content.
- `nuxt-simple-sitemap` or custom sitemap endpoint returning all product/category URLs.

---

## Sprint 16 — Reports & Analytics Dashboard

**Sprint Goal:** Admin có dashboard báo cáo doanh thu, đơn hàng, sản phẩm bán chạy.

**Stories:**

- RPT-001: Revenue Dashboard (daily/weekly/monthly)
- RPT-002: Order Report (by status, date range)
- RPT-003: Best-Selling Products Report
- RPT-004: Low-Stock Inventory Report
- RPT-005: Customer Growth Report
- RPT-006: Promotion Effectiveness Report
- RPT-007: Payment Method Distribution Report
- RPT-008: Return Rate Report
- HOME-004: Best Sellers Section on Homepage

---

## Sprint 17 — Admin UX Polish & Audit Log

**Stories:**

- PERM-003: Audit Log Viewer (Admin)
- PERM-004: Activity log for all admin actions
- ADMIN-001: Bulk Product Status Update
- ADMIN-002: Bulk Price Update
- ADMIN-003: Admin User Deactivation
- ADMIN-004: Admin Session Management
- PROD-011: Clone Product
- PROD-012: Bulk Product Status Update
- PROD-013: Bulk Price Update

---

## Sprint 18 — Performance & Caching

**Stories:**

- PERF-001: Redis Caching for Product/Catalog
- Database query optimization audit
- Image CDN configuration optimization
- API response time monitoring

---

## Sprint 19 — Observability

**Stories:**

- OBS-002: Error Monitoring (Sentry)
- OBS-004: APM / Performance Tracing
- OBS-005: Slow Query Logging
- Alerting rules setup
- Dashboard for ops team

---

## Sprint 20 — Personalization & Recommendations

**Stories:**

- HOME-006: Personalized Recommendations
- Product recommendation engine (collaborative filtering)
- "Customers also bought" on PDP
- Personalized email campaigns
