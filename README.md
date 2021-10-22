# Project .NET Framework

* Naam: Al-Timimi Ali
* Studentennummer: 0151149-23
* Academiejaar: 21-22
* Klasgroep: INF201A
* Onderwerp: Language - IDE

## Sprint 3

### Beide zoekcriteria ingevuld
```
SELECT "i"."Id", "i"."Manufacturer", "i"."Name", "i"."Price", "i"."ReleaseDate", "i"."SupportedLanguages"
FROM "Ides" AS "i"
WHERE ((@__name_0 = '') OR (instr("i"."Name", @__name_0) > 0)) AND (CAST(strftime('%Y', "i"."ReleaseDate") AS INTEGER) = @__releaseYear_1)
```

### Enkel zoeken op naam
```
SELECT "i"."Id", "i"."Manufacturer", "i"."Name", "i"."Price", "i"."ReleaseDate", "i"."SupportedLanguages"
FROM "Ides" AS "i"
WHERE (@__name_0 = '') OR (instr("i"."Name", @__name_0) > 0)
```
### Enkel zoeken op releaseYear
```
SELECT "i"."Id", "i"."Manufacturer", "i"."Name", "i"."Price", "i"."ReleaseDate", "i"."SupportedLanguages"
FROM "Ides" AS "i"
WHERE CAST(strftime('%Y', "i"."ReleaseDate") AS INTEGER) = @__releaseYear_0
```
### Beide zoekcriteria leeg
```
SELECT "i"."Id", "i"."Manufacturer", "i"."Name", "i"."Price", "i"."ReleaseDate", "i"."SupportedLanguages"
FROM "Ides" AS "i"
```