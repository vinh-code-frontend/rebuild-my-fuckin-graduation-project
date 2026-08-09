# Project Progress Tracker

> Cập nhật file này khi bắt đầu / hoàn thành sprint hoặc story bất kỳ.  
> Roadmap đầy đủ: [docs/roadmap/README.md](./roadmap/README.md)

---

## Current Status

| Field                | Value                                  |
| -------------------- | -------------------------------------- |
| **Current Sprint**   | Sprint 0 — Foundation & Infrastructure |
| **Phase**            | MVP                                    |
| **Sprint Start**     | —                                      |
| **Sprint End**       | —                                      |
| **Overall Progress** | 0 / 20 sprints completed               |

---

## Sprint Progress

| Sprint    | Name                               | Phase    | Status         | Notes |
| --------- | ---------------------------------- | -------- | -------------- | ----- |
| Sprint 0  | Foundation & Infrastructure        | MVP      | ✅ Done        |       |
| Sprint 1  | Authentication & RBAC              | MVP      | 🔄 In Progress |       |
| Sprint 2  | Catalog                            | MVP      | ⬜ Not Started |       |
| Sprint 3  | Product Management + Listing       | MVP      | ⬜ Not Started |       |
| Sprint 4  | Product Detail + Variants + Images | MVP      | ⬜ Not Started |       |
| Sprint 5  | Search & Discovery                 | MVP      | ⬜ Not Started |       |
| Sprint 6  | Cart                               | MVP      | ⬜ Not Started |       |
| Sprint 7  | Checkout + Address                 | MVP      | ⬜ Not Started |       |
| Sprint 8  | Order Management                   | MVP      | ⬜ Not Started |       |
| Sprint 9  | Inventory + COD Payment            | MVP      | ⬜ Not Started |       |
| Sprint 10 | Promotion & Coupon                 | Post-MVP | ⬜ Not Started |       |
| Sprint 11 | Online Payment Gateway             | Post-MVP | ⬜ Not Started |       |
| Sprint 12 | Customer Account + Wishlist        | Post-MVP | ⬜ Not Started |       |
| Sprint 13 | Reviews + Q&A + Comparison         | Post-MVP | ⬜ Not Started |       |
| Sprint 14 | Return, Refund, Warranty           | Post-MVP | ⬜ Not Started |       |
| Sprint 15 | Content Management + SEO           | Post-MVP | ⬜ Not Started |       |
| Sprint 16 | Reports & Analytics                | Advanced | ⬜ Not Started |       |
| Sprint 17 | Admin Polish + Audit Log           | Advanced | ⬜ Not Started |       |
| Sprint 18 | Performance & Caching              | Advanced | ⬜ Not Started |       |
| Sprint 19 | Observability                      | Advanced | ⬜ Not Started |       |
| Sprint 20 | Personalization                    | Future   | ⬜ Not Started |       |

> Status icons: ✅ Done | 🔄 In Progress | ⬜ Not Started | 🚫 Blocked | ⏭️ Skipped

---

## Sprint 0 — Foundation & Infrastructure

**Goal:** Thiết lập project structure, DB schema, CI pipeline, coding conventions.

| Story ID  | Story                                | Status | Notes                                                               |
| --------- | ------------------------------------ | ------ | ------------------------------------------------------------------- |
| FOUND-001 | Database schema design               | ✅     |                                                                     |
| FOUND-002 | EF Core setup + migrations           | ✅     |                                                                     |
| FOUND-003 | Global exception handling middleware | ✅     | Already implemented                                                 |
| FOUND-004 | Logging infrastructure (Serilog)     | ✅     |                                                                     |
| FOUND-005 | Scalar / OpenAPI setup               | ✅     |                                                                     |
| FOUND-006 | CORS configuration                   | ✅     |                                                                     |
| FOUND-007 | Nuxt project bootstrap               | ✅     |                                                                     |
| FOUND-008 | React Admin bootstrap                | ✅     |                                                                     |
| FOUND-009 | Environment config                   | ✅     |                                                                     |
| FOUND-010 | CI pipeline                          | ⏭️     | Skipped                                                             |
| OBS-001   | Structured logging (Serilog)         | ✅     |                                                                     |
| OBS-003   | Health check endpoints               | ✅     |                                                                     |
| PERF-005  | API pagination enforcement           | ⏭️     | Skipped. handle this while implementing user management in sprint 1 |

**Sprint 0 Completion:** 13 / 13 (skiped 2)

---

## Sprint 1 — Authentication & RBAC

**Goal:** Customer register/login/logout. Admin login with RBAC.

| Story ID   | Story                      | Status | Notes                         |
| ---------- | -------------------------- | ------ | ----------------------------- |
| AUTH-001   | Customer registration      | ⬜     | Already partially implemented |
| AUTH-002   | Customer login             | ⬜     |                               |
| AUTH-003   | Customer logout            | ⬜     |                               |
| AUTH-004   | Forgot password            | ⬜     |                               |
| AUTH-005   | Reset password             | ⬜     |                               |
| AUTH-006   | JWT + Refresh Token        | ⬜     | Already partially implemented |
| AUTH-007   | Admin login                | ⬜     | Already partially implemented |
| AUTH-008   | Admin RBAC — roles         | ⬜     |                               |
| AUTH-009   | Admin RBAC — permissions   | ⬜     |                               |
| PERM-001   | Create admin user          | ⬜     |                               |
| PERM-002   | Manage roles & assignments | ⬜     |                               |
| USER-001   | Customer profile page      | ⬜     |                               |
| USER-002   | Update profile             | ⬜     |                               |
| USER-006   | Customer list (admin)      | ⬜     |                               |
| USER-007   | Customer detail (admin)    | ⬜     |                               |
| USER-010   | Change password            | ⬜     |                               |
| NOTIFY-003 | Password reset email       | ⬜     |                               |

**Sprint 1 Completion:** 0 / 17

---

## Blocked Items

> List any stories currently blocked and their blockers.

| Story ID | Story | Blocker | Since |
| -------- | ----- | ------- | ----- |
| —        | —     | —       | —     |

---

## Decisions & Notes

> Ghi lại các quyết định kỹ thuật hoặc product đã được confirm.

| Date       | Decision        | Context          |
| ---------- | --------------- | ---------------- |
| 2026-08-09 | Roadmap created | Initial planning |

---

## Open Questions Status

> Từ [docs/roadmap/07-context.md](./roadmap/07-context.md) — cập nhật khi có quyết định.

| #     | Question                                   | Status  | Decision                            |
| ----- | ------------------------------------------ | ------- | ----------------------------------- |
| OQ-01 | Shipping fee: flat rate hay real-time API? | ⬜ Open | —                                   |
| OQ-02 | Guest checkout cho MVP?                    | ⬜ Open | —                                   |
| OQ-03 | Installment payment cho MVP?               | ⬜ Open | —                                   |
| OQ-04 | Category hierarchy levels tối đa?          | ⬜ Open | Proposed: 3                         |
| OQ-07 | Email service provider?                    | ⬜ Open | —                                   |
| OQ-08 | Object storage provider?                   | ⬜ Open | MinIO cho local dev                 |
| OQ-09 | Multi-warehouse cho MVP?                   | ⬜ Open | Lean towards: No (single warehouse) |
| OQ-13 | Store pickup cho MVP?                      | ⬜ Open | —                                   |

---

## Changelog

| Date       | Change                           | Author |
| ---------- | -------------------------------- | ------ |
| 2026-08-09 | Initial progress tracker created | AI     |
