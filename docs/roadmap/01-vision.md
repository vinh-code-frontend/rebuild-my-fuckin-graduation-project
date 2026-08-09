# 01 — Product Vision, Scope, Personas & Architecture

[← README](./README.md)

---

## 1. Product Vision

> **"Xây dựng nền tảng thương mại điện tử bán điện thoại và thiết bị công nghệ hàng đầu, cung cấp trải nghiệm mua hàng liền mạch từ khám phá sản phẩm đến giao hàng, đồng thời trang bị cho đội ngũ nội bộ công cụ quản lý vận hành hiệu quả và tin cậy."**

### Mission

Cung cấp cho khách hàng nền tảng mua điện thoại và thiết bị công nghệ đáng tin cậy nhất, với giá minh bạch, thông tin sản phẩm đầy đủ, thanh toán đa dạng và giao hàng nhanh chóng.

### Strategic Goals

- **G1** — Customer Acquisition: Thu hút khách hàng qua SEO, promotion và product discovery.
- **G2** — Conversion Optimization: Tối ưu funnel từ product view → add-to-cart → checkout → payment.
- **G3** — Retention: Xây dựng loyalty thông qua trải nghiệm sau mua (order tracking, warranty, support).
- **G4** — Operational Efficiency: Trang bị Admin Portal đủ mạnh để staff vận hành nhanh, ít lỗi.
- **G5** — Scalability: Kiến trúc có thể mở rộng khi tăng trafic, SKU và đơn hàng.

---

## 2. Product Scope

### In-Scope (Platform)

| Layer            | System               | Audience                              |
| ---------------- | -------------------- | ------------------------------------- |
| Customer Website | Nuxt (SSR)           | End Users / Customers                 |
| Admin Portal     | React                | Internal Staff / Administrators       |
| Backend API      | .NET / ASP.NET Core  | Internal (consumed by both frontends) |
| Database         | Microsoft SQL Server | Backend                               |

### Product Lifecycle Coverage

```
Product Discovery → Product Detail → Search → Compare
→ Cart → Checkout → Payment → Order
→ Delivery → Customer Account
→ Promotion → Warranty → Return / Refund → Customer Support
```

### Admin Operations Coverage

```
Product Management → Catalog Management → Inventory → Pricing
→ Promotion → Order Management → Customer Management
→ Content Management → User & Permission → Reports → System Configuration
```

### Out of Scope (Current Phase)

- Physical POS integration
- Warehouse management system (WMS) integration
- ERP integration
- B2B / wholesale features
- Multi-vendor marketplace
- Mobile app (iOS / Android)
- International shipping
- Multi-currency
- Multi-language (Vietnamese only for MVP)

---

## 3. Personas

### P1 — Customer (End User)

- **Name:** Minh, 25 tuổi, sinh viên / nhân viên văn phòng
- **Goals:** Tìm điện thoại phù hợp ngân sách, so sánh models, đặt hàng nhanh, nhận hàng đúng hẹn.
- **Pain Points:** Thông tin không đầy đủ, giá thay đổi lúc checkout, giao hàng chậm, khó theo dõi đơn hàng.
- **Devices:** Mobile (primary), Desktop (secondary)
- **SEO Touchpoint:** Google search "mua iPhone 15 giá tốt", "so sánh Samsung vs iPhone"

### P2 — Admin / Product Manager

- **Name:** Linh, 30 tuổi, nhân viên quản lý sản phẩm
- **Goals:** Tạo và cập nhật sản phẩm nhanh, quản lý giá và khuyến mãi, theo dõi tồn kho.
- **Pain Points:** Hệ thống chậm, khó bulk update, thiếu thông báo hết hàng.

### P3 — Order Operations Staff

- **Name:** Hùng, 28 tuổi, nhân viên xử lý đơn hàng
- **Goals:** Xử lý đơn hàng nhanh, cập nhật trạng thái, xử lý return/refund.
- **Pain Points:** Thiếu timeline đơn hàng, không có workflow rõ ràng cho cancellation.

### P4 — Customer Support Staff

- **Name:** Mai, 26 tuổi, nhân viên CSKH
- **Goals:** Tra cứu thông tin đơn hàng, trả lời câu hỏi của khách, xử lý khiếu nại.
- **Pain Points:** Không có đủ thông tin khách hàng, không thấy lịch sử mua hàng.

### P5 — System Administrator

- **Name:** Nam, 35 tuổi, IT Admin
- **Goals:** Quản lý tài khoản nội bộ, phân quyền, giám sát hệ thống.
- **Pain Points:** Thiếu audit log, khó revoke quyền khi có sự cố.

---

## 4. Product Modules

```
┌─────────────────────────────────────────────────────────────┐
│                    CUSTOMER WEBSITE (Nuxt)                   │
├──────────┬──────────┬──────────┬──────────┬─────────────────┤
│ Homepage │ Catalog  │  Search  │  Product │  Cart/Checkout  │
│ & CMS    │  & PLP   │          │  Detail  │  & Payment      │
├──────────┴──────────┴──────────┴──────────┴─────────────────┤
│ Order │ Account │ Wishlist │ Reviews │ Compare │ Support     │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                    ADMIN PORTAL (React)                      │
├──────────┬──────────┬──────────┬──────────┬─────────────────┤
│Dashboard │ Products │ Catalog  │Inventory │    Pricing      │
├──────────┼──────────┼──────────┼──────────┼─────────────────┤
│Promotion │  Orders  │Customers │  Users   │    Content      │
├──────────┴──────────┴──────────┴──────────┴─────────────────┤
│              Reports & Analytics                             │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                    BACKEND API (.NET)                        │
├──────────┬──────────┬──────────┬──────────┬─────────────────┤
│   Auth   │  Product │  Catalog │Inventory │     Cart        │
├──────────┼──────────┼──────────┼──────────┼─────────────────┤
│ Checkout │  Payment │  Order   │ Promotion│    Review       │
├──────────┼──────────┼──────────┼──────────┼─────────────────┤
│  Return  │ Warranty │ Customer │  Content │    Reports      │
├──────────┴──────────┴──────────┴──────────┴─────────────────┤
│  Notification │ Search │ User & Permission │ Audit Log      │
└─────────────────────────────────────────────────────────────┘
```

---

## 5. Architecture Overview

### System Boundaries

```
┌──────────────┐     HTTPS      ┌─────────────────────────────┐
│ Nuxt Client  │ ─────────────► │                             │
│  (SSR/SPA)   │                │   ASP.NET Core Web API      │
└──────────────┘                │                             │
                                │  ┌───────────────────────┐  │
┌──────────────┐     HTTPS      │  │  Application Layer    │  │
│ React Admin  │ ─────────────► │  │  (Services/Use Cases) │  │
│   Portal     │                │  └───────────────────────┘  │
└──────────────┘                │  ┌───────────────────────┐  │
                                │  │  Domain Layer         │  │
                                │  │  (Entities/BizRules)  │  │
                                │  └───────────────────────┘  │
                                │  ┌───────────────────────┐  │
                                │  │  Infrastructure       │  │
                                │  │  (EF Core, Repos,     │  │
                                │  │   External Clients)   │  │
                                │  └───────────────────────┘  │
                                └──────────────┬──────────────┘
                                               │
                    ┌──────────────────────────┼──────────────────────┐
                    │                          │                      │
               ┌────▼────┐           ┌─────────▼────┐    ┌───────────▼────────┐
               │  MSSQL  │           │  Redis Cache │    │  Object Storage    │
               │  (EF)   │           │  (Optional)  │    │  (Images/Files)    │
               └─────────┘           └──────────────┘    └────────────────────┘
```

### Layer Responsibilities

| Layer                 | Responsibility                                                                               |
| --------------------- | -------------------------------------------------------------------------------------------- |
| **Nuxt Client**       | SSR pages, SEO meta, customer UI, state management (Pinia), API calls via `useFetch`         |
| **React Admin**       | SPA admin dashboard, product/order/user management UI, form validation, role-based UI hiding |
| **.NET API**          | Request routing, authentication middleware, input validation, response shaping               |
| **Application Layer** | Use cases, business logic orchestration, DTOs, service interfaces                            |
| **Domain Layer**      | Entities, value objects, domain events, business rules enforcement                           |
| **Infrastructure**    | EF Core repositories, external API clients (payment, shipping, email), caching adapters      |
| **SQL Server**        | Persistence, constraints, indexes, transactions                                              |

### Architecture Pattern

- **Clean Architecture** with layered structure (already established in codebase)
- **Monolithic API** (Modular Monolith acceptable for MVP scale)
- Avoid microservices unless a specific module clearly warrants it
