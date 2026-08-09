# 08 — Complete Backlog

[← README](./README.md)

> MVP Backlog, Post-MVP Backlog, Future Roadmap, Epic Summary, Sprint Summary, Full Backlog Table.

---

## 22. MVP Backlog

Stories required to launch MVP end-to-end:

| ID               | Story                                                                      | Epic          |
| ---------------- | -------------------------------------------------------------------------- | ------------- |
| FOUND-001–010    | Foundation & Infrastructure                                                | Foundation    |
| AUTH-001–006     | Customer Auth (Register, Login, Logout, Forgot/Reset Password, JWT)        | Auth          |
| AUTH-007–009     | Admin Login + RBAC                                                         | Auth          |
| PERM-001–002     | Admin User + Role Management                                               | Permissions   |
| CAT-001–010      | Category CRUD + Hierarchy + Brand + Attributes + Navigation                | Catalog       |
| PROD-001–006     | Product CRUD + Publish + SEO                                               | Product       |
| PROD-007–010     | Variants + Images + Specs                                                  | Product       |
| PLP-001–005      | Product Listing Page + Filter + Sort + Pagination                          | Client        |
| PDP-001–008      | Product Detail Page + Gallery + Variants + Specs + Breadcrumb              | Client        |
| HOME-001–002     | Static Banner + Featured Products                                          | Homepage      |
| SEARCH-001–007   | Keyword Search + Autocomplete + Filter + Sort + Empty State                | Search        |
| INV-001–007      | Stock View + Adjustment + Reservation + Release + Expiry + Alert + History | Inventory     |
| CART-001–008     | Full Cart Flow + Persistence + Validation + Guest Merge                    | Cart          |
| CHECKOUT-001–008 | Full Checkout Flow + Address                                               | Checkout      |
| USER-001–008     | Profile + Address Management + Customer List (Admin)                       | Customer      |
| USER-010         | Change Password                                                            | Customer      |
| ORDER-001–009    | Full Order Management (Admin + Customer)                                   | Order         |
| PAY-001–003      | COD Payment Flow                                                           | Payment       |
| PROMO-001–006    | Coupon CRUD + Apply + Validate + Track                                     | Promotion     |
| NOTIFY-001–003   | Order Confirmation + Status Update + Password Reset Email                  | Notifications |
| PERF-002–005     | CDN + Image Optimization + Pagination                                      | Performance   |
| OBS-001, OBS-003 | Structured Logging + Health Check                                          | Observability |

---

## 23. Post-MVP Backlog

| ID                     | Story                                                             | Sprint          |
| ---------------------- | ----------------------------------------------------------------- | --------------- |
| PROMO-007–008          | Flash Sale (Admin + Client display)                               | Sprint 10       |
| PROMO-012–013          | Free Shipping + Campaign Management                               | Sprint 10       |
| PAY-004–010            | Online Payment Gateway (full flow + idempotency + reconciliation) | Sprint 11       |
| WISH-001–004           | Wishlist                                                          | Sprint 12       |
| REC-001                | Recently Viewed                                                   | Sprint 12       |
| NOTIFY-004–005         | Low Stock Alert + SMS                                             | Sprint 12       |
| USER-009, ORDER-010    | Account Dashboard + Re-order                                      | Sprint 12       |
| SEARCH-006, SEARCH-008 | Search History + Keyword Highlight                                | Sprint 12/13    |
| REVIEW-001–006         | Reviews & Ratings (submit + moderate + helpfulness)               | Sprint 13       |
| QA-001–003             | Product Q&A                                                       | Sprint 13       |
| COMPARE-001–003        | Product Comparison                                                | Sprint 13       |
| PLP-008, PDP-009–012   | Rating Filter + Installment + Warranty + Video + Accessories      | Sprint 13/14    |
| RETURN-001–006         | Return & Refund (full flow)                                       | Sprint 14       |
| WARRANTY-001–003       | Warranty Info + Claim                                             | Sprint 14       |
| CMS-001–010            | Content Management + Blog + FAQ + SEO Pages                       | Sprint 15       |
| HOME-003, HOME-007–008 | Flash Sale Section + Recently Viewed + Brand Showcase             | Sprint 10/12/15 |
| RPT-001–008            | Reports & Analytics Dashboard                                     | Sprint 16       |
| PERM-003–004           | Audit Log                                                         | Sprint 17       |
| ADMIN-001–004          | Bulk Operations + Admin Session Management                        | Sprint 17       |
| PROD-011–013           | Clone Product + Bulk Update                                       | Sprint 17       |

---

## 24. Future Roadmap

| Feature                         | Description                                          |
| ------------------------------- | ---------------------------------------------------- |
| Personalized Recommendations    | ML-based (collaborative filtering)                   |
| Advanced Search                 | Elasticsearch / Meilisearch with Vietnamese NLP      |
| Multi-language                  | English support                                      |
| Mobile App                      | React Native or Flutter                              |
| Loyalty Program                 | Points, tiers, rewards                               |
| Affiliate Program               | Referral tracking                                    |
| Bundle Products                 | Phone + case + screen protector                      |
| Pre-order                       | Orders for upcoming products                         |
| Live Chat                       | Real-time customer support widget                    |
| B2B / Wholesale                 | Tiered pricing for business customers                |
| Multi-warehouse                 | Multiple fulfillment centers                         |
| ERP Integration                 | Sync inventory and orders                            |
| Click & Collect                 | Buy online, pick up in store                         |
| Trade-in Program                | Old device trade-in during purchase                  |
| Product Launch Notifications    | "Notify me when available"                           |
| Advanced Promotions             | Stacking, tiered discounts, member-exclusive pricing |
| Customer Segmentation Campaigns | Targeted promotions                                  |
| Predictive Inventory            | AI-driven restocking alerts                          |

---

## 25. Epic Summary

| Epic                              | Priority | MVP          | Stories (est.) | Sprint(s)                          |
| --------------------------------- | -------- | ------------ | -------------- | ---------------------------------- |
| EPIC-FOUND — Foundation           | P0       | ✅           | 10             | Sprint 0                           |
| EPIC-AUTH — Authentication & RBAC | P0       | ✅           | 9              | Sprint 1                           |
| EPIC-CAT — Catalog Management     | P0       | ✅           | 10             | Sprint 2                           |
| EPIC-PROD — Product Management    | P0       | ✅           | 12             | Sprint 3–4                         |
| EPIC-SEARCH — Search & Discovery  | P0       | ✅           | 8              | Sprint 5                           |
| EPIC-CART — Cart                  | P0       | ✅           | 8              | Sprint 6                           |
| EPIC-CHECKOUT — Checkout          | P0       | ✅           | 8              | Sprint 7                           |
| EPIC-ORDER — Order Management     | P0       | ✅           | 10             | Sprint 8                           |
| EPIC-INV — Inventory Management   | P0       | ✅           | 7              | Sprint 9                           |
| EPIC-PAY — Payment                | P0       | ✅ (COD)     | 11             | Sprint 9 (COD), Sprint 11 (Online) |
| EPIC-USER — Customer Account      | P1       | ✅ (partial) | 10             | Sprint 1, 7, 12                    |
| EPIC-PROMO — Promotion & Coupon   | P1       | ✅ (basic)   | 10             | Sprint 10                          |
| EPIC-NOTIFY — Notifications       | P1       | ✅ (email)   | 5              | Sprint 9, 12                       |
| EPIC-REVIEW — Reviews & Ratings   | P2       | ❌           | 6              | Sprint 13                          |
| EPIC-COMPARE — Product Comparison | P2       | ❌           | 3              | Sprint 13                          |
| EPIC-RETURN — Return & Refund     | P1       | ❌           | 6              | Sprint 14                          |
| EPIC-WARRANTY — Warranty          | P2       | ❌           | 3              | Sprint 14                          |
| EPIC-CMS — Content Management     | P2       | ❌           | 10             | Sprint 15                          |
| EPIC-RPT — Reports & Analytics    | P2       | ❌           | 8              | Sprint 16                          |
| EPIC-PERM — Admin Permissions     | P1       | ✅ (basic)   | 4              | Sprint 1, 17                       |
| EPIC-WISH — Wishlist              | P2       | ❌           | 4              | Sprint 12                          |
| EPIC-QA — Product Q&A             | P3       | ❌           | 3              | Sprint 13                          |

---

## 26. Sprint Summary

| Sprint    | Goal                                        | Stories (est.) | Phase    | Key Dependencies |
| --------- | ------------------------------------------- | -------------- | -------- | ---------------- |
| Sprint 0  | Foundation & Infrastructure                 | 10             | MVP      | None             |
| Sprint 1  | Auth & RBAC                                 | 14             | MVP      | Sprint 0         |
| Sprint 2  | Catalog                                     | 10             | MVP      | Sprint 0         |
| Sprint 3  | Product Management + Listing                | 12             | MVP      | Sprint 2         |
| Sprint 4  | Product Detail + Variants + Images          | 14             | MVP      | Sprint 3         |
| Sprint 5  | Search & Discovery                          | 8              | MVP      | Sprint 3         |
| Sprint 6  | Cart                                        | 8              | MVP      | Sprint 4         |
| Sprint 7  | Checkout + Address                          | 9              | MVP      | Sprint 1, 6      |
| Sprint 8  | Order Management                            | 12             | MVP      | Sprint 7         |
| Sprint 9  | Inventory + COD Payment + Email             | 12             | MVP      | Sprint 8         |
| Sprint 10 | Promotion & Coupon                          | 12             | Post-MVP | Sprint 7, 9      |
| Sprint 11 | Online Payment Gateway                      | 8              | Post-MVP | Sprint 8, 9      |
| Sprint 12 | Customer Account + Wishlist + Notifications | 12             | Post-MVP | Sprint 1, 8      |
| Sprint 13 | Reviews + Q&A + Comparison                  | 14             | Post-MVP | Sprint 4, 8      |
| Sprint 14 | Return, Refund, Warranty                    | 11             | Post-MVP | Sprint 8, 11     |
| Sprint 15 | Content Management + SEO                    | 12             | Post-MVP | Sprint 3         |
| Sprint 16 | Reports & Analytics Dashboard               | 9              | Advanced | Sprint 8, 9, 10  |
| Sprint 17 | Admin Polish + Audit Log + Permissions      | 9              | Advanced | Sprint 1         |
| Sprint 18 | Performance & Caching                       | 6              | Advanced | All              |
| Sprint 19 | Observability                               | 5              | Advanced | All              |
| Sprint 20 | Personalization + Recommendations           | 6              | Future   | Sprint 12, 16    |

---

## 27. Complete Product Backlog

| ID           | Epic          | Story                                   | Priority | Phase    | Sprint | MVP |
| ------------ | ------------- | --------------------------------------- | -------- | -------- | ------ | --- |
| FOUND-001    | Foundation    | Database schema design                  | P0       | MVP      | 0      | ✅  |
| FOUND-002    | Foundation    | EF Core setup + migrations              | P0       | MVP      | 0      | ✅  |
| FOUND-003    | Foundation    | Global exception handling middleware    | P0       | MVP      | 0      | ✅  |
| FOUND-004    | Foundation    | Logging infrastructure (Serilog)        | P0       | MVP      | 0      | ✅  |
| FOUND-005    | Foundation    | Swagger / OpenAPI setup                 | P1       | MVP      | 0      | ✅  |
| FOUND-006    | Foundation    | CORS configuration                      | P0       | MVP      | 0      | ✅  |
| FOUND-007    | Foundation    | Nuxt project bootstrap                  | P0       | MVP      | 0      | ✅  |
| FOUND-008    | Foundation    | React Admin bootstrap                   | P0       | MVP      | 0      | ✅  |
| FOUND-009    | Foundation    | Environment config                      | P0       | MVP      | 0      | ✅  |
| FOUND-010    | Foundation    | CI pipeline                             | P1       | MVP      | 0      | ✅  |
| AUTH-001     | Auth          | Customer registration                   | P0       | MVP      | 1      | ✅  |
| AUTH-002     | Auth          | Customer login                          | P0       | MVP      | 1      | ✅  |
| AUTH-003     | Auth          | Customer logout                         | P0       | MVP      | 1      | ✅  |
| AUTH-004     | Auth          | Forgot password                         | P0       | MVP      | 1      | ✅  |
| AUTH-005     | Auth          | Reset password                          | P0       | MVP      | 1      | ✅  |
| AUTH-006     | Auth          | JWT + Refresh Token                     | P0       | MVP      | 1      | ✅  |
| AUTH-007     | Auth          | Admin login                             | P0       | MVP      | 1      | ✅  |
| AUTH-008     | Auth          | Admin RBAC — roles                      | P0       | MVP      | 1      | ✅  |
| AUTH-009     | Auth          | Admin RBAC — permissions on endpoints   | P0       | MVP      | 1      | ✅  |
| PERM-001     | Permissions   | Create admin user                       | P0       | MVP      | 1      | ✅  |
| PERM-002     | Permissions   | Manage roles & assignments              | P0       | MVP      | 1      | ✅  |
| PERM-003     | Permissions   | Audit log viewer                        | P1       | Advanced | 17     | ❌  |
| PERM-004     | Permissions   | Admin activity log                      | P1       | Advanced | 17     | ❌  |
| CAT-001      | Catalog       | Create category                         | P0       | MVP      | 2      | ✅  |
| CAT-002      | Catalog       | Update category                         | P0       | MVP      | 2      | ✅  |
| CAT-003      | Catalog       | Delete category (soft)                  | P0       | MVP      | 2      | ✅  |
| CAT-004      | Catalog       | Category hierarchy (tree)               | P0       | MVP      | 2      | ✅  |
| CAT-005      | Catalog       | Create brand                            | P0       | MVP      | 2      | ✅  |
| CAT-006      | Catalog       | Update brand                            | P0       | MVP      | 2      | ✅  |
| CAT-007      | Catalog       | Delete brand (soft)                     | P1       | MVP      | 2      | ✅  |
| CAT-008      | Catalog       | Manage product attributes               | P0       | MVP      | 2      | ✅  |
| CAT-009      | Catalog       | Manage attribute values                 | P0       | MVP      | 2      | ✅  |
| CAT-010      | Catalog       | Category navigation on client           | P0       | MVP      | 2      | ✅  |
| PROD-001     | Product       | Create product                          | P0       | MVP      | 3      | ✅  |
| PROD-002     | Product       | Update product                          | P0       | MVP      | 3      | ✅  |
| PROD-003     | Product       | Delete product (soft)                   | P0       | MVP      | 3      | ✅  |
| PROD-004     | Product       | Publish / Unpublish product             | P0       | MVP      | 3      | ✅  |
| PROD-005     | Product       | Admin product list (search + filter)    | P0       | MVP      | 3      | ✅  |
| PROD-006     | Product       | Product SEO management                  | P1       | MVP      | 3      | ✅  |
| PROD-007     | Product       | Create product variant (SKU)            | P0       | MVP      | 4      | ✅  |
| PROD-008     | Product       | Update product variant                  | P0       | MVP      | 4      | ✅  |
| PROD-009     | Product       | Upload product images                   | P0       | MVP      | 4      | ✅  |
| PROD-010     | Product       | Manage product specifications           | P1       | MVP      | 4      | ✅  |
| PROD-011     | Product       | Clone product                           | P2       | Post-MVP | 17     | ❌  |
| PROD-012     | Product       | Bulk product status update              | P2       | Advanced | 17     | ❌  |
| PROD-013     | Product       | Bulk price update                       | P2       | Advanced | 17     | ❌  |
| PLP-001      | Client        | Product listing page                    | P0       | MVP      | 3      | ✅  |
| PLP-002      | Client        | Category filter on PLP                  | P0       | MVP      | 3      | ✅  |
| PLP-003      | Client        | Brand filter on PLP                     | P0       | MVP      | 3      | ✅  |
| PLP-004      | Client        | Sort by price/newest                    | P0       | MVP      | 3      | ✅  |
| PLP-005      | Client        | Pagination on PLP                       | P0       | MVP      | 3      | ✅  |
| PLP-006      | Client        | Price range filter                      | P1       | MVP      | 5      | ✅  |
| PLP-007      | Client        | Availability filter                     | P1       | MVP      | 5      | ✅  |
| PLP-008      | Client        | Rating filter on PLP                    | P2       | Post-MVP | 13     | ❌  |
| PDP-001      | Client        | Product detail page                     | P0       | MVP      | 4      | ✅  |
| PDP-002      | Client        | Product image gallery                   | P0       | MVP      | 4      | ✅  |
| PDP-003      | Client        | Variant selection                       | P0       | MVP      | 4      | ✅  |
| PDP-004      | Client        | Price display (base + sale)             | P0       | MVP      | 4      | ✅  |
| PDP-005      | Client        | Stock availability indicator            | P0       | MVP      | 4      | ✅  |
| PDP-006      | Client        | Product specifications table            | P0       | MVP      | 4      | ✅  |
| PDP-007      | Client        | Breadcrumb navigation                   | P1       | MVP      | 4      | ✅  |
| PDP-008      | Client        | Related products section                | P1       | MVP      | 4      | ✅  |
| PDP-009      | Client        | Installment information display         | P2       | Post-MVP | 11     | ❌  |
| PDP-010      | Client        | Warranty information display            | P2       | Post-MVP | 14     | ❌  |
| PDP-011      | Client        | Product video support                   | P2       | Post-MVP | 15     | ❌  |
| PDP-012      | Client        | Accessories section                     | P2       | Post-MVP | 13     | ❌  |
| SEARCH-001   | Search        | Keyword search API                      | P0       | MVP      | 5      | ✅  |
| SEARCH-002   | Search        | Search results page                     | P0       | MVP      | 5      | ✅  |
| SEARCH-003   | Search        | Autocomplete suggestions                | P1       | MVP      | 5      | ✅  |
| SEARCH-004   | Search        | Filter on search results                | P1       | MVP      | 5      | ✅  |
| SEARCH-005   | Search        | Sort on search results                  | P1       | MVP      | 5      | ✅  |
| SEARCH-006   | Search        | Search history                          | P2       | Post-MVP | 12     | ❌  |
| SEARCH-007   | Search        | Empty search result state               | P1       | MVP      | 5      | ✅  |
| SEARCH-008   | Search        | Search result keyword highlight         | P2       | Post-MVP | 13     | ❌  |
| INV-001      | Inventory     | View stock level per SKU                | P0       | MVP      | 9      | ✅  |
| INV-002      | Inventory     | Manual stock adjustment                 | P0       | MVP      | 9      | ✅  |
| INV-003      | Inventory     | Stock reservation at order creation     | P0       | MVP      | 9      | ✅  |
| INV-004      | Inventory     | Release reservation on cancel           | P0       | MVP      | 9      | ✅  |
| INV-005      | Inventory     | Release reservation on expiry (job)     | P0       | MVP      | 9      | ✅  |
| INV-006      | Inventory     | Low stock alert                         | P1       | MVP      | 9      | ✅  |
| INV-007      | Inventory     | Stock movement history                  | P1       | MVP      | 9      | ✅  |
| INV-008      | Inventory     | Warehouse management (multi-warehouse)  | P3       | Future   | —      | ❌  |
| CART-001     | Cart          | Add product to cart                     | P0       | MVP      | 6      | ✅  |
| CART-002     | Cart          | Update cart item quantity               | P0       | MVP      | 6      | ✅  |
| CART-003     | Cart          | Remove cart item                        | P0       | MVP      | 6      | ✅  |
| CART-004     | Cart          | View cart summary                       | P0       | MVP      | 6      | ✅  |
| CART-005     | Cart          | Cart persistence                        | P0       | MVP      | 6      | ✅  |
| CART-006     | Cart          | Stock validation in cart                | P0       | MVP      | 6      | ✅  |
| CART-007     | Cart          | Price re-validation on cart view        | P0       | MVP      | 6      | ✅  |
| CART-008     | Cart          | Merge guest cart on login               | P1       | MVP      | 6      | ✅  |
| CHECKOUT-001 | Checkout      | Customer info form                      | P0       | MVP      | 7      | ✅  |
| CHECKOUT-002 | Checkout      | Select saved address                    | P0       | MVP      | 7      | ✅  |
| CHECKOUT-003 | Checkout      | Add new address at checkout             | P0       | MVP      | 7      | ✅  |
| CHECKOUT-004 | Checkout      | Select delivery method                  | P0       | MVP      | 7      | ✅  |
| CHECKOUT-005 | Checkout      | Shipping fee calculation                | P0       | MVP      | 7      | ✅  |
| CHECKOUT-006 | Checkout      | Order summary display                   | P0       | MVP      | 7      | ✅  |
| CHECKOUT-007 | Checkout      | Place order (COD)                       | P0       | MVP      | 7      | ✅  |
| CHECKOUT-008 | Checkout      | Order confirmation page                 | P0       | MVP      | 7      | ✅  |
| CHECKOUT-009 | Checkout      | Store pickup option                     | P2       | Future   | —      | ❌  |
| USER-001     | Customer      | Customer profile page                   | P0       | MVP      | 1      | ✅  |
| USER-002     | Customer      | Update profile                          | P0       | MVP      | 1      | ✅  |
| USER-003     | Customer      | Manage saved addresses                  | P0       | MVP      | 7      | ✅  |
| USER-004     | Customer      | Add / edit address                      | P0       | MVP      | 7      | ✅  |
| USER-005     | Customer      | Set default address                     | P1       | MVP      | 7      | ✅  |
| USER-006     | Customer      | Customer list (admin)                   | P0       | MVP      | 1      | ✅  |
| USER-007     | Customer      | Customer detail (admin)                 | P0       | MVP      | 1      | ✅  |
| USER-008     | Customer      | Customer order history (admin view)     | P1       | MVP      | 8      | ✅  |
| USER-009     | Customer      | Account dashboard page                  | P1       | Post-MVP | 12     | ❌  |
| USER-010     | Customer      | Change password                         | P1       | MVP      | 1      | ✅  |
| ORDER-001    | Order         | Order list (admin)                      | P0       | MVP      | 8      | ✅  |
| ORDER-002    | Order         | Order detail (admin)                    | P0       | MVP      | 8      | ✅  |
| ORDER-003    | Order         | Update order status (admin)             | P0       | MVP      | 8      | ✅  |
| ORDER-004    | Order         | Order activity/timeline log             | P0       | MVP      | 8      | ✅  |
| ORDER-005    | Order         | Cancel order (admin)                    | P0       | MVP      | 8      | ✅  |
| ORDER-006    | Order         | Customer order history page             | P0       | MVP      | 8      | ✅  |
| ORDER-007    | Order         | Customer order detail page              | P0       | MVP      | 8      | ✅  |
| ORDER-008    | Order         | Order status tracking (customer)        | P0       | MVP      | 8      | ✅  |
| ORDER-009    | Order         | Cancel order (customer — pending only)  | P1       | MVP      | 8      | ✅  |
| ORDER-010    | Order         | Re-order                                | P2       | Post-MVP | 12     | ❌  |
| PAY-001      | Payment       | Cash on delivery flow                   | P0       | MVP      | 9      | ✅  |
| PAY-002      | Payment       | Mark order as paid (admin — COD)        | P0       | MVP      | 9      | ✅  |
| PAY-003      | Payment       | Payment status tracking                 | P0       | MVP      | 9      | ✅  |
| PAY-004      | Payment       | Payment gateway integration             | P0       | Post-MVP | 11     | ❌  |
| PAY-005      | Payment       | Payment redirect flow                   | P0       | Post-MVP | 11     | ❌  |
| PAY-006      | Payment       | Payment callback / webhook handling     | P0       | Post-MVP | 11     | ❌  |
| PAY-007      | Payment       | Payment idempotency                     | P0       | Post-MVP | 11     | ❌  |
| PAY-008      | Payment       | Pending payment timeout handling        | P0       | Post-MVP | 11     | ❌  |
| PAY-009      | Payment       | Payment failed handling                 | P0       | Post-MVP | 11     | ❌  |
| PAY-010      | Payment       | Payment reconciliation job              | P1       | Post-MVP | 11     | ❌  |
| PAY-011      | Payment       | Installment payment display             | P2       | Future   | —      | ❌  |
| PROMO-001    | Promotion     | Create coupon (admin)                   | P1       | MVP      | 10     | ✅  |
| PROMO-002    | Promotion     | Update / deactivate coupon              | P1       | MVP      | 10     | ✅  |
| PROMO-003    | Promotion     | Coupon list (admin)                     | P1       | MVP      | 10     | ✅  |
| PROMO-004    | Promotion     | Apply coupon at checkout                | P1       | MVP      | 10     | ✅  |
| PROMO-005    | Promotion     | Server-side coupon validation           | P0       | MVP      | 10     | ✅  |
| PROMO-006    | Promotion     | Coupon usage tracking                   | P1       | MVP      | 10     | ✅  |
| PROMO-007    | Promotion     | Flash sale — admin create               | P1       | Post-MVP | 10     | ❌  |
| PROMO-008    | Promotion     | Flash sale — client display + countdown | P1       | Post-MVP | 10     | ❌  |
| PROMO-009    | Promotion     | Product-level sale price (in variant)   | P0       | MVP      | 4      | ✅  |
| PROMO-010    | Promotion     | Buy X Get Y                             | P2       | Future   | —      | ❌  |
| PROMO-011    | Promotion     | Bundle discount                         | P2       | Future   | —      | ❌  |
| PROMO-012    | Promotion     | Free shipping promotion                 | P2       | Post-MVP | 10     | ❌  |
| PROMO-013    | Promotion     | Campaign management                     | P2       | Post-MVP | 10     | ❌  |
| NOTIFY-001   | Notifications | Order confirmation email                | P1       | MVP      | 9      | ✅  |
| NOTIFY-002   | Notifications | Order status update email               | P1       | MVP      | 9      | ✅  |
| NOTIFY-003   | Notifications | Password reset email                    | P0       | MVP      | 1      | ✅  |
| NOTIFY-004   | Notifications | Low stock alert email (admin)           | P1       | Post-MVP | 12     | ❌  |
| NOTIFY-005   | Notifications | SMS OTP / order notification            | P2       | Post-MVP | 12     | ❌  |
| NOTIFY-006   | Notifications | Push notification (mobile)              | P3       | Future   | —      | ❌  |
| WISH-001     | Wishlist      | Add to wishlist                         | P2       | Post-MVP | 12     | ❌  |
| WISH-002     | Wishlist      | View wishlist                           | P2       | Post-MVP | 12     | ❌  |
| WISH-003     | Wishlist      | Remove from wishlist                    | P2       | Post-MVP | 12     | ❌  |
| WISH-004     | Wishlist      | Move wishlist item to cart              | P2       | Post-MVP | 12     | ❌  |
| REVIEW-001   | Review        | Submit review (verified purchase)       | P2       | Post-MVP | 13     | ❌  |
| REVIEW-002   | Review        | Rating system                           | P2       | Post-MVP | 13     | ❌  |
| REVIEW-003   | Review        | Upload review images                    | P2       | Post-MVP | 13     | ❌  |
| REVIEW-004   | Review        | Review moderation (admin)               | P2       | Post-MVP | 13     | ❌  |
| REVIEW-005   | Review        | Verified purchase badge                 | P2       | Post-MVP | 13     | ❌  |
| REVIEW-006   | Review        | Review helpfulness vote                 | P3       | Post-MVP | 13     | ❌  |
| QA-001       | Q&A           | Ask product question                    | P3       | Post-MVP | 13     | ❌  |
| QA-002       | Q&A           | Answer question (admin)                 | P3       | Post-MVP | 13     | ❌  |
| QA-003       | Q&A           | Q&A display on PDP                      | P3       | Post-MVP | 13     | ❌  |
| COMPARE-001  | Compare       | Add to compare                          | P2       | Post-MVP | 13     | ❌  |
| COMPARE-002  | Compare       | Product comparison page                 | P2       | Post-MVP | 13     | ❌  |
| COMPARE-003  | Compare       | Highlight specification differences     | P2       | Post-MVP | 13     | ❌  |
| RETURN-001   | Return        | Submit return request                   | P1       | Post-MVP | 14     | ❌  |
| RETURN-002   | Return        | Return request list (admin)             | P1       | Post-MVP | 14     | ❌  |
| RETURN-003   | Return        | Approve / reject return                 | P1       | Post-MVP | 14     | ❌  |
| RETURN-004   | Return        | Process refund                          | P1       | Post-MVP | 14     | ❌  |
| RETURN-005   | Return        | Refund to original payment method       | P1       | Post-MVP | 14     | ❌  |
| RETURN-006   | Return        | Return status tracking (customer)       | P1       | Post-MVP | 14     | ❌  |
| WARRANTY-001 | Warranty      | Warranty info on PDP                    | P2       | Post-MVP | 14     | ❌  |
| WARRANTY-002 | Warranty      | Warranty claim request (customer)       | P2       | Post-MVP | 14     | ❌  |
| WARRANTY-003 | Warranty      | Warranty claim management (admin)       | P2       | Post-MVP | 14     | ❌  |
| CMS-001      | CMS           | Homepage banner management              | P2       | Post-MVP | 15     | ❌  |
| CMS-002      | CMS           | Navigation menu configuration           | P2       | Post-MVP | 15     | ❌  |
| CMS-003      | CMS           | SEO metadata per page                   | P1       | Post-MVP | 15     | ❌  |
| CMS-004      | CMS           | Sitemap generation                      | P1       | Post-MVP | 15     | ❌  |
| CMS-005      | CMS           | Robots.txt configuration                | P1       | Post-MVP | 15     | ❌  |
| CMS-006      | CMS           | Structured data                         | P1       | Post-MVP | 15     | ❌  |
| CMS-007      | CMS           | Open Graph tags                         | P1       | Post-MVP | 15     | ❌  |
| CMS-008      | CMS           | Blog / news CRUD (admin)                | P2       | Post-MVP | 15     | ❌  |
| CMS-009      | CMS           | Blog list + detail page (client)        | P2       | Post-MVP | 15     | ❌  |
| CMS-010      | CMS           | FAQ management + FAQ page               | P2       | Post-MVP | 15     | ❌  |
| RPT-001      | Reports       | Revenue dashboard                       | P2       | Advanced | 16     | ❌  |
| RPT-002      | Reports       | Order report                            | P2       | Advanced | 16     | ❌  |
| RPT-003      | Reports       | Best-selling products report            | P2       | Advanced | 16     | ❌  |
| RPT-004      | Reports       | Low-stock inventory report              | P1       | Advanced | 16     | ❌  |
| RPT-005      | Reports       | Customer growth report                  | P2       | Advanced | 16     | ❌  |
| RPT-006      | Reports       | Promotion effectiveness report          | P2       | Advanced | 16     | ❌  |
| RPT-007      | Reports       | Payment method distribution             | P2       | Advanced | 16     | ❌  |
| RPT-008      | Reports       | Return rate report                      | P2       | Advanced | 16     | ❌  |
| ADMIN-001    | Admin UX      | Bulk product status update              | P2       | Advanced | 17     | ❌  |
| ADMIN-002    | Admin UX      | Bulk price update                       | P2       | Advanced | 17     | ❌  |
| ADMIN-003    | Admin UX      | Admin user deactivation                 | P1       | Advanced | 17     | ❌  |
| ADMIN-004    | Admin UX      | Admin session management                | P1       | Advanced | 17     | ❌  |
| HOME-001     | Homepage      | Hero banner display (static)            | P1       | MVP      | 3      | ✅  |
| HOME-002     | Homepage      | Featured products section               | P1       | MVP      | 3      | ✅  |
| HOME-003     | Homepage      | Flash sale section                      | P2       | Post-MVP | 10     | ❌  |
| HOME-004     | Homepage      | Best sellers section                    | P2       | Post-MVP | 16     | ❌  |
| HOME-005     | Homepage      | New arrivals section                    | P2       | Post-MVP | 3      | ❌  |
| HOME-006     | Homepage      | Personalized recommendations            | P3       | Future   | 20     | ❌  |
| HOME-007     | Homepage      | Recently viewed products                | P2       | Post-MVP | 12     | ❌  |
| HOME-008     | Homepage      | Brand showcase section                  | P2       | Post-MVP | 15     | ❌  |
| PERF-001     | Performance   | Redis caching for product/catalog       | P2       | Advanced | 18     | ❌  |
| PERF-002     | Performance   | CDN integration for images              | P1       | MVP      | 4      | ✅  |
| PERF-003     | Performance   | Image compression (WebP + srcset)       | P1       | MVP      | 4      | ✅  |
| PERF-004     | Performance   | Lazy loading images                     | P1       | MVP      | 4      | ✅  |
| PERF-005     | Performance   | API response pagination enforcement     | P0       | MVP      | 0      | ✅  |
| OBS-001      | Observability | Structured logging (Serilog)            | P1       | MVP      | 0      | ✅  |
| OBS-002      | Observability | Error monitoring (Sentry)               | P1       | Advanced | 19     | ❌  |
| OBS-003      | Observability | Health check endpoints                  | P1       | MVP      | 0      | ✅  |
| OBS-004      | Observability | APM / performance tracing               | P2       | Advanced | 19     | ❌  |
| OBS-005      | Observability | Slow query logging                      | P1       | Advanced | 19     | ❌  |

---

_Total estimated stories: ~160+_  
_MVP stories: ~85_  
_Post-MVP stories: ~45_  
_Advanced/Future stories: ~30+_
