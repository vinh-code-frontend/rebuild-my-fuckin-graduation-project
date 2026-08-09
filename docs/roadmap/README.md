# Product Roadmap — E-Commerce Platform (Phone & Electronics)

> **Version:** 1.0 | **Date:** 2026-08-09 | **Status:** Draft

---

## Quick Navigation

| #   | File                                             | Nội dung                                                                      |
| --- | ------------------------------------------------ | ----------------------------------------------------------------------------- |
| 01  | [Vision & Architecture](./01-vision.md)          | Product Vision, Scope, Personas, Modules, Architecture                        |
| 02  | [MVP & Roadmap](./02-mvp-roadmap.md)             | MVP Definition, Phase timeline                                                |
| 03  | [Sprint Planning](./03-sprints.md)               | Chi tiết từng sprint (Sprint 0–17)                                            |
| 04  | [Epics & Stories](./04-stories.md)               | Epics & User Stories chi tiết                                                 |
| 05  | [Business Rules & Technical](./05-rules-tech.md) | Business Rules, Dependencies, Technical Considerations, Security              |
| 06  | [Non-Functional Requirements](./06-nfr.md)       | Performance, SEO, Analytics & Metrics, Edge Cases                             |
| 07  | [Integrations & Risks](./07-context.md)          | External Integrations, Risks, Open Questions, Assumptions                     |
| 08  | [Complete Backlog](./08-backlog.md)              | MVP/Post-MVP/Future Backlog, Epic Summary, Sprint Summary, Full Backlog Table |

---

## 📍 Progress Tracker

Xem trạng thái triển khai hiện tại tại: [../progress.md](../progress.md)

---

## Sprint Overview

```
Phase 1 — MVP (Sprint 0–9)
  Sprint 0  Foundation & Infrastructure
  Sprint 1  Authentication & RBAC
  Sprint 2  Catalog (Categories, Brands, Attributes)
  Sprint 3  Product Management + Product Listing
  Sprint 4  Product Detail + Variants + Images
  Sprint 5  Search & Discovery
  Sprint 6  Cart
  Sprint 7  Checkout + Address
  Sprint 8  Order Management
  Sprint 9  Inventory + COD Payment

Phase 2 — Post-MVP (Sprint 10–15)
  Sprint 10 Promotion & Coupon
  Sprint 11 Online Payment Gateway
  Sprint 12 Customer Account + Wishlist + Notifications
  Sprint 13 Reviews + Q&A + Comparison
  Sprint 14 Return, Refund, Warranty
  Sprint 15 Content Management + SEO

Phase 3 — Advanced (Sprint 16–20)
  Sprint 16 Reports & Analytics
  Sprint 17 Admin Polish + Audit Log
  Sprint 18 Performance & Caching
  Sprint 19 Observability
  Sprint 20 Personalization
```

---

## Story ID Conventions

| Prefix      | Epic                          |
| ----------- | ----------------------------- |
| `FOUND-`    | Foundation                    |
| `AUTH-`     | Authentication                |
| `PERM-`     | Permissions & RBAC            |
| `CAT-`      | Catalog (Categories, Brands)  |
| `PROD-`     | Product Management            |
| `PLP-`      | Product Listing Page (Client) |
| `PDP-`      | Product Detail Page (Client)  |
| `SEARCH-`   | Search & Discovery            |
| `INV-`      | Inventory                     |
| `CART-`     | Cart                          |
| `CHECKOUT-` | Checkout                      |
| `USER-`     | Customer Account              |
| `ORDER-`    | Order Management              |
| `PAY-`      | Payment                       |
| `PROMO-`    | Promotion & Coupon            |
| `NOTIFY-`   | Notifications                 |
| `WISH-`     | Wishlist                      |
| `REVIEW-`   | Reviews & Ratings             |
| `QA-`       | Product Q&A                   |
| `COMPARE-`  | Product Comparison            |
| `RETURN-`   | Return & Refund               |
| `WARRANTY-` | Warranty                      |
| `CMS-`      | Content Management            |
| `RPT-`      | Reports                       |
| `HOME-`     | Homepage                      |
| `PERF-`     | Performance                   |
| `OBS-`      | Observability                 |
