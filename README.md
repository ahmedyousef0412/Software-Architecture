# Layered Architecture (N-Tier)

##  Overview
Layered architecture organizes code into logical layers:
- **Presentation**: Handles HTTP requests (controllers, views).
- **Application**: Orchestrates use cases.
- **Domain**: Core business rules, entities, aggregates.
- **Infrastructure**: Database, external services.



##  When to Use
- Small to medium systems.
- Applications with clear separation of concerns.
- Enterprise apps with CRUD-heavy operations.

##  Limitations
- Can become rigid in large systems.
- Tends to push business logic into the Application if not disciplined.
