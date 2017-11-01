# FhirQuery
Research into a possible language for querying FHIR servers

## Idea
Combine the strenght of existing languages to fill the gap of missing features, while trying not to reinvent the wheel. 
This idea serves to provide an alternative to GraphQL which is another good candidate to solve this problem.  

## Proposal
- Stay as close as possible to existing standard SQL.
- Use Json to create tree structures (DocumentDB language is a nice example)
- Use FhirPath for projection (select)
- Use existing FHIR search parameters for filtering (where)

## Some examples

### SQL Compatible
In terms of input format, this query still adheres to regular SQL 
```SQL
select name, birthDate from Patient where id = 4
```
### Usage of FhirPath for projection
Fhir path statements return either a scalar or a collection of substructures of a resource. They can be used to build custom XML and JSON structures, but do not necessarily  flatten the output hierarchy.
```SQL
select 
    name.family, 
    name.given[0]
from
    Patient
```

### Using FHIR Search parameters
FHIR search parameters themselves flatten the hierarchy of their corresponding resource, and are a natural fit for filtering.
```SQL
select * from Patient where email = 'patientzero@test.com'
```

