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

Example queries:
In terms of input format, this query still adheres to regular SQL 
```SQL
select name, birthDate from Patient where id = 4
```

Useage of FhirPath for projection:
```SQL
select 
    name.family, 
    name.given[0]
from
    Patient
```
