# 02 — MVP Definition & Product Roadmap

[← README](./README.md)

---

## 6. MVP Definition

### MVP Goal

Cho phép khách hàng duyệt sản phẩm, tìm kiếm, thêm vào giỏ hàng, thanh toán và theo dõi đơn hàng. Đồng thời cho phép admin quản lý sản phẩm, tồn kho, đơn hàng cơ bản.

### MVP — Customer Must Have

| #   | Feature                                  |
| --- | ---------------------------------------- |
| ✅  | Register / Login / Logout                |
| ✅  | Browse product categories                |
| ✅  | Product listing with basic filter & sort |
| ✅  | Product detail page with variants        |
| ✅  | Keyword search                           |
| ✅  | Add to cart / update / remove            |
| ✅  | Checkout with address                    |
| ✅  | Cash on delivery payment                 |
| ✅  | Order confirmation                       |
| ✅  | View order history & detail              |
| ✅  | Basic order tracking (status)            |
| ✅  | Customer profile & address               |
| ✅  | Forgot password / reset password         |

### MVP — Admin Must Have

| #   | Feature                                    |
| --- | ------------------------------------------ |
| ✅  | Admin login (RBAC)                         |
| ✅  | Create / update / delete / publish product |
| ✅  | Manage product variants & SKUs             |
| ✅  | Upload product images                      |
| ✅  | Manage categories & brands                 |
| ✅  | View & adjust stock                        |
| ✅  | View order list & order detail             |
| ✅  | Update order status                        |
| ✅  | View customer list                         |
| ✅  | Create basic coupon                        |

### MVP Exclusions (Post-MVP)

- Payment gateway (credit card, e-wallet) — MVP uses COD only
- Advanced promotion (Flash Sale, Buy X Get Y, Bundle)
- Product comparison
- Product reviews
- Wishlist
- Product Q&A
- Return / Refund workflow
- Warranty management
- Advanced reports
- Content management (banners, CMS)
- Personalized recommendations
- Notifications (email/SMS)

> **Note:** Email order confirmation is borderline MVP — include if email integration is low effort. Mark as P1/Should Have.

---

## 7. Product Roadmap

```
Phase 1 — Foundation & MVP (Sprint 0–9)
┌────────────────────────────────────────────────────────────────┐
│ Sprint 0  Foundation, DevOps, DB Schema, CI pipeline           │
│ Sprint 1  Authentication & RBAC                                │
│ Sprint 2  Catalog — Categories, Brands, Attributes             │
│ Sprint 3  Product Management (Admin) + Product Listing (Client)│
│ Sprint 4  Product Detail + Variants + Images                   │
│ Sprint 5  Search & Filter                                      │
│ Sprint 6  Cart                                                 │
│ Sprint 7  Checkout + Address                                   │
│ Sprint 8  Order Management                                     │
│ Sprint 9  Inventory Management + COD Payment                   │
└────────────────────────────────────────────────────────────────┘

Phase 2 — Post-MVP (Sprint 10–15)
┌────────────────────────────────────────────────────────────────┐
│ Sprint 10 Promotion & Coupon                                   │
│ Sprint 11 Payment Gateway (Online Payment)                     │
│ Sprint 12 Customer Account (Wishlist, Recently Viewed, Notif.) │
│ Sprint 13 Reviews, Q&A, Product Comparison                     │
│ Sprint 14 Return, Refund, Warranty                             │
│ Sprint 15 Content Management + SEO Optimization                │
└────────────────────────────────────────────────────────────────┘

Phase 3 — Advanced (Sprint 16–20)
┌────────────────────────────────────────────────────────────────┐
│ Sprint 16 Advanced Reports & Analytics Dashboard               │
│ Sprint 17 Admin UX Polish + Audit Log + Permissions Refinement │
│ Sprint 18 Performance: Caching, CDN, Image Optimization        │
│ Sprint 19 Observability: Logging, Metrics, Error Monitoring    │
│ Sprint 20 Personalization + Recommendations                    │
└────────────────────────────────────────────────────────────────┘
```

### Dependency Chain

```
Foundation
    ↓
Auth & RBAC ─── Catalog
                    ↓
              Product Management
                    ↓
            ┌───────┴────────┐
          Search           PDP + Variants
                                ↓
                              Cart
                                ↓
                           Checkout ── Inventory + COD
                                ↓
                          Order Management
                                ↓
                    ┌─────────┬────────┐
                 Promotion  Payment  Customer Account
                              ↓
                          Return & Refund
```
