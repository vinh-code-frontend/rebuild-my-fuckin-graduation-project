# 06 — Non-Functional Requirements

[← README](./README.md)

> Performance, SEO, Analytics & Metrics, Edge Cases.

---

## 14. Performance Requirements

### API Response Time Targets

| Endpoint Category            | Target P95                        |
| ---------------------------- | --------------------------------- |
| Product listing / search     | < 200ms                           |
| Product detail               | < 150ms                           |
| Cart operations              | < 200ms                           |
| Checkout / Order creation    | < 500ms                           |
| Admin dashboard (aggregates) | < 1s                              |
| Report queries               | < 3s (async background if needed) |

### Database

- All FK columns indexed.
- Avoid N+1 queries — use EF Core `.Include()` judiciously; prefer projection queries for lists.
- Pagination on all list endpoints (default 20, max 100 per page).
- Slow query threshold: 500ms → log and alert.

### Frontend (Nuxt)

| Concern                        | Target                            |
| ------------------------------ | --------------------------------- |
| Nuxt SSR Time to First Byte    | < 200ms                           |
| Largest Contentful Paint (LCP) | < 2.5s                            |
| Cumulative Layout Shift (CLS)  | < 0.1                             |
| First Input Delay (FID)        | < 100ms                           |
| Image Optimization             | WebP format, lazy loading, srcset |
| Code Splitting                 | Route-based splits                |

### CDN & Images

- Product images served from CDN.
- Image srcset: 480w, 800w, 1200w.
- `loading="lazy"` for below-fold images.
- `fetchpriority="high"` on hero/above-fold images.

---

## 15. SEO Requirements

### Nuxt SSR Configuration

| Requirement           | Implementation                                                      |
| --------------------- | ------------------------------------------------------------------- |
| Server-Side Rendering | `nuxt.config.ts` — `ssr: true` (default)                            |
| Dynamic Meta Tags     | `useHead()` / `useSeoMeta()` per page                               |
| Open Graph            | `og:title`, `og:description`, `og:image`, `og:url` on all key pages |
| Twitter Card          | `twitter:card`, `twitter:title`, `twitter:image`                    |

### Structured Data (JSON-LD)

| Page           | Schema                                                                                     |
| -------------- | ------------------------------------------------------------------------------------------ |
| Product Detail | `@type: Product` with `name`, `image`, `description`, `brand`, `offers`, `aggregateRating` |
| Category / PLP | `@type: ItemList` with `ListItem` per product                                              |
| Breadcrumb     | `@type: BreadcrumbList` on PLP and PDP                                                     |
| Organization   | Homepage — `@type: Organization`                                                           |

### URL Structure

| Page     | URL Pattern                                |
| -------- | ------------------------------------------ |
| Category | `/dien-thoai`, `/may-tinh-bang`            |
| Brand    | `/dien-thoai/apple`, `/dien-thoai/samsung` |
| Product  | `/dien-thoai/iphone-15-pro`                |
| Search   | `/tim-kiem?q=iphone`                       |
| Order    | `/tai-khoan/don-hang/{orderId}`            |
| Blog     | `/tin-tuc/{slug}`                          |

### Technical SEO

| Requirement     | Detail                                                                                |
| --------------- | ------------------------------------------------------------------------------------- |
| Canonical URL   | `<link rel="canonical">` on all pages; pagination uses `rel="next/prev"`              |
| Sitemap         | `/sitemap.xml` auto-generated: all published products, categories, brands, blog posts |
| Robots.txt      | Disallow `/admin`, `/tai-khoan`, `/gio-hang`, `/thanh-toan`                           |
| 404 Handling    | Custom 404 page; return HTTP 404 status (not 200)                                     |
| 301 Redirects   | Product/category slug changes trigger 301 redirect                                    |
| Hreflang        | Vietnamese only for MVP — single `vi` hreflang                                        |
| Core Web Vitals | Monitor LCP, CLS, FID via analytics integration                                       |

---

## 16. Analytics & Metrics

### Key Product Metrics

| Metric                    | Definition                                      | Why Track                | Event                   |
| ------------------------- | ----------------------------------------------- | ------------------------ | ----------------------- |
| Product View              | User views PDP                                  | Top/bottom performers    | `product_viewed`        |
| Add to Cart Rate          | Add-to-cart / Product views                     | Intent friction          | `add_to_cart`           |
| Checkout Started          | Sessions entering checkout / sessions with cart | Pre-checkout abandonment | `checkout_started`      |
| Checkout Completion Rate  | Orders placed / Checkout started                | Checkout UX quality      | `order_placed`          |
| Conversion Rate           | Orders / Sessions                               | Overall funnel health    | composite               |
| Cart Abandonment Rate     | 1 - (Orders / Add-to-cart)                      | Checkout friction        | composite               |
| Average Order Value (AOV) | Total Revenue / Order Count                     | Revenue efficiency       | composite               |
| Search Click-through Rate | Clicks on search results / Total searches       | Search quality           | `search_result_clicked` |
| Search with no results    | Searches returning 0 results                    | Catalog gaps             | `search_no_results`     |
| DAU / MAU                 | Daily / Monthly active users                    | Growth & retention       | session events          |
| Repeat Purchase Rate      | Customers with >1 order / Total                 | Retention quality        | composite               |
| Promotion Conversion      | Orders using promo / Total orders               | Campaign effectiveness   | `promo_applied`         |
| Return Rate               | Return requests / Orders                        | Product quality          | composite               |

### Events to Emit (Client-side)

```
page_viewed           { pageType, pageId }
product_viewed        { productId, variantId, price, source }
add_to_cart           { variantId, quantity, price, source }
remove_from_cart      { variantId, quantity }
checkout_started      { cartValue, itemCount }
checkout_step_viewed  { step }
order_placed          { orderId, total, itemCount, paymentMethod, couponUsed }
search_performed      { query, resultCount }
search_result_clicked { query, productId, position }
search_no_results     { query }
category_viewed       { categoryId, categoryName }
promotion_applied     { couponCode, discountAmount }
review_submitted      { productId, rating }
```

---

## 17. Edge Cases

### Inventory

| Case                                                | Handling                                            |
| --------------------------------------------------- | --------------------------------------------------- |
| 2 customers buy last SKU simultaneously             | DB-level conditional UPDATE — only first succeeds   |
| Customer adds 5 to cart, 3 sell out before checkout | Warn at checkout; cap quantity at available         |
| Reservation expires, customer returns to checkout   | Re-check stock; show "Sản phẩm đã hết hàng"         |
| Admin adjusts stock while customer has reservation  | Alert admin, don't auto-release reservations        |
| Order cancelled after stock adjustment              | Only release reservation, not adjust physical stock |

### Payment

| Case                                                      | Handling                                                              |
| --------------------------------------------------------- | --------------------------------------------------------------------- |
| Payment success but API timeout returns error to frontend | Reconciliation job detects success → update order, notify customer    |
| Payment callback sent twice                               | IdempotencyKey check — second callback ignored                        |
| Payment gateway down                                      | Return "Payment service temporarily unavailable"; do not create order |
| Payment amount mismatch (tampered client)                 | Server validates amount = order total before initiating               |
| Refund fails at gateway                                   | Mark as Failed; alert admin; allow manual retry                       |

### Order

| Case                                           | Handling                                                      |
| ---------------------------------------------- | ------------------------------------------------------------- |
| Customer cancels Pending order (COD)           | Status → Cancelled; stock released; no payment processing     |
| Customer cancels order after payment           | Status → Cancelled; trigger refund process                    |
| Cancel after Shipped                           | Not allowed via cancel — must submit return request           |
| Partial return                                 | Supported at item level; refund = sum of returned item values |
| Order with promo — promo expires before cancel | Coupon usage decremented on cancel regardless                 |

### Promotion

| Case                                        | Handling                                              |
| ------------------------------------------- | ----------------------------------------------------- |
| Coupon expired at order creation time       | Reject with "Coupon đã hết hạn"                       |
| Coupon quota exhausted simultaneously       | Atomic DB update; last-quota customer wins            |
| Product in coupon scope becomes unavailable | Discount not applied for unavailable items            |
| Multiple promotions same product            | MVP: one coupon only. Post-MVP: define stacking rules |

### Product

| Case                                   | Handling                                                 |
| -------------------------------------- | -------------------------------------------------------- |
| Product unpublished while in cart      | Show warning; block checkout for that item               |
| Variant deactivated while in cart      | Same as above                                            |
| Price increases while at checkout page | Re-validate at placement; show price-changed banner      |
| Product images fail to load            | Show placeholder; do not break layout                    |
| Slug collision on create               | Auto-append suffix: `iphone-15-pro-1`, `iphone-15-pro-2` |

### Search

| Case                        | Handling                                                     |
| --------------------------- | ------------------------------------------------------------ |
| Search query injection      | Parameterized queries only; sanitize before Full-Text Search |
| Empty search query          | Show popular products                                        |
| Extremely long query        | Truncate at 200 chars with user notice                       |
| Special characters in query | Strip or escape; do not error                                |
