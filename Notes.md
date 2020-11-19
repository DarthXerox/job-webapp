# Notes  

## Filters:

- JobOffers by tag/skill
- JobOffers by Company name
- JobOffers by City (?)
- JobOffers by name (regex/substring/keywords)
- Companies by name

Each of the above should also provide OrderBy
JobSeekers by skills? (probably not)
How about companies by skill?

## Facades

### JobOffers (Puf)
- Apply
- List
- Search (by skill)
- Add (only companies)
- Edit (only companies)
- Remove (only companies)

### JobSeekers (Martin)
- Register (add)
- Edit info
- Get info

### JobApplication (Kika)
- List
- Remove
- ApplyToJobOffer
- Accept/Reject Applicant (only companies)
- ListApplicantsForJobOffer (only companies)

### Company (Martin)
- List
- Add
- Edit data




add at least 2-3 query classes, more when implementing BL

## Query
https://www.rahulpnath.com/blog/query-object-pattern-and-entity-framework-making-readable-queries/
for each possible "dotaz" exists specific Query class
