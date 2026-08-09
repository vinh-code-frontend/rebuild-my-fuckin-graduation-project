# 07 — External Integrations, Risks, Open Questions & Assumptions

[← README](./README.md)

---

## 18. External Integrations

### Required for MVP

| Integration                          | Purpose                            | Notes                              |
| ------------------------------------ | ---------------------------------- | ---------------------------------- |
| Object Storage (S3/Azure Blob/MinIO) | Product image storage              | Required — local disk not suitable |
| Email (SMTP / SendGrid)              | Order confirmation, password reset | Transactional email only           |

### Optional for MVP (Should Have)

| Integration                     | Purpose                         | Notes              |
| ------------------------------- | ------------------------------- | ------------------ |
| CDN (Cloudflare/AWS CloudFront) | Image and static asset delivery | Highly recommended |

### Required for Post-MVP

| Integration                              | Purpose                          | Sprint                          |
| ---------------------------------------- | -------------------------------- | ------------------------------- |
| Payment Gateway (VNPAY/MoMo/ZaloPay)     | Online payment                   | Sprint 11                       |
| Shipping Provider (GHTK/GHN/ViettelPost) | Real-time shipping fee, tracking | Sprint 7 uses fixed fee for MVP |
| SMS Gateway (Twilio/ESMS)                | OTP, order notifications         | Sprint 12                       |
| Push Notifications (Firebase FCM)        | Mobile push                      | Sprint 12                       |

### Future Integrations

| Integration                 | Purpose                                 |
| --------------------------- | --------------------------------------- |
| Elasticsearch / Meilisearch | Advanced Vietnamese search              |
| Analytics (GA4 / Mixpanel)  | Product analytics                       |
| Error Monitoring (Sentry)   | Frontend + backend error tracking       |
| APM (Datadog / New Relic)   | Performance monitoring                  |
| CAPTCHA (reCAPTCHA v3)      | Bot protection on registration/checkout |
| ERP Integration             | Enterprise inventory sync               |

---

## 19. Risks

| ID   | Risk                                    | Probability | Impact   | Mitigation                                                      |
| ---- | --------------------------------------- | ----------- | -------- | --------------------------------------------------------------- |
| R-01 | Overselling due to race condition       | High        | Critical | Atomic DB updates + optimistic concurrency                      |
| R-02 | Payment double-charge                   | Medium      | Critical | Idempotency keys + gateway reconciliation                       |
| R-03 | Search quality poor for Vietnamese text | High        | Medium   | Plan migration to Meilisearch post-MVP                          |
| R-04 | Admin portal security breach            | Low         | Critical | RBAC at API level, audit log, session timeout                   |
| R-05 | Slow product listing under load         | Medium      | High     | DB indexes, caching, pagination enforcement                     |
| R-06 | Image storage costs                     | Low         | Medium   | CDN + compressed WebP; lifecycle policies on S3                 |
| R-07 | Payment gateway integration delays      | Medium      | High     | Start early (Sprint 11); mock during dev                        |
| R-08 | SEO indexing failure (SSR errors)       | Medium      | High     | Test SSR with Googlebot simulator; no 500 errors                |
| R-09 | Cart / checkout abandonment             | High        | Medium   | Track abandonment events; cart recovery emails post-MVP         |
| R-10 | Schema migration complexity at scale    | Low         | Medium   | EF Core migrations with rollback scripts; test on staging first |

---

## 20. Open Questions

| #     | Question                                                                     | Owner                 | Priority |
| ----- | ---------------------------------------------------------------------------- | --------------------- | -------- |
| OQ-01 | Shipping fee: flat rate or real-time API (GHN/GHTK) for MVP?                 | Product + Engineering | High     |
| OQ-02 | Guest checkout: allowed for MVP?                                             | Product               | High     |
| OQ-03 | Installment payment: required for MVP? (Typical in Vietnamese mobile retail) | Product               | High     |
| OQ-04 | Max number of category hierarchy levels? (Proposed: 3)                       | Product               | Medium   |
| OQ-05 | Return window: 7 or 30 days?                                                 | Business              | Medium   |
| OQ-06 | Does "low stock" threshold differ per product category?                      | Product               | Low      |
| OQ-07 | Email service provider: SendGrid vs AWS SES vs SMTP?                         | Engineering           | Medium   |
| OQ-08 | Object storage: AWS S3 vs Azure Blob vs MinIO?                               | Engineering           | High     |
| OQ-09 | Multi-warehouse inventory for MVP or single-warehouse only?                  | Business              | High     |
| OQ-10 | Warranty: self-managed or manufacturer integration?                          | Business              | Medium   |
| OQ-11 | Blog/News: required for MVP SEO or Post-MVP?                                 | Product               | Low      |
| OQ-12 | Product Q&A: customer-to-customer or admin-only answers?                     | Product               | Low      |
| OQ-13 | Store pickup (click & collect) for MVP?                                      | Business              | High     |
| OQ-14 | Audit log retention period?                                                  | Compliance            | Low      |
| OQ-15 | Coupon stacking: can multiple coupons be used on one order?                  | Product               | Medium   |

---

## 21. Assumptions

| #    | Assumption                                                                          |
| ---- | ----------------------------------------------------------------------------------- |
| A-01 | Vietnamese language only for MVP (no multi-language support)                        |
| A-02 | Single warehouse / single inventory pool for MVP                                    |
| A-03 | Single currency: VND                                                                |
| A-04 | Guest checkout is supported (no mandatory login to purchase)                        |
| A-05 | COD is the only payment method for MVP; online payment in Sprint 11                 |
| A-06 | Flat-rate shipping fee for MVP; real-time shipping API is Post-MVP                  |
| A-07 | Product comparison limited to phones (same category); max 4 products                |
| A-08 | Product variants: Color + Storage for phones; other attributes for other categories |
| A-09 | Admin portal is desktop-only; no mobile admin requirement                           |
| A-10 | Email required for Order Confirmation; SMS is Post-MVP                              |
| A-11 | Search uses SQL Server Full-Text Search for MVP; Elasticsearch is future            |
| A-12 | No real-time features (WebSocket) for MVP                                           |
| A-13 | MinIO for local dev; production object storage TBD                                  |
| A-14 | Analytics tracking (GA4) added Post-MVP; MVP focuses on data model readiness        |
