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

## Sprint 6

### Nieuwe Software

#### Request
POST https://localhost:5001/api/Softwares
Content-Type: application/json

{"name": "test","description": "test#2"}

#### Response
HTTP/1.1 201 Created
Date: Wed, 29 Dec 2021 22:51:08 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked
Location: https://localhost:5001/api/Softwares/6

{
"id": 6,
"name": "test",
"description": "test#2",
"languageUsed": null
}

### Aanpassen van een software (success)

#### Request
PUT https://localhost:5001/api/Softwares/3
Content-Type: application/json

{
"id": 4,
"name": "testcall1",
"description": "testdescput1"
}

#### Response
HTTP/1.1 204 No Content
Date: Sun, 02 Jan 2022 21:06:28 GMT
Server: Kestrel



### Aanpassen van een software (fail)

#### Request
PUT https://localhost:5001/api/Softwares/3
Content-Type: application/json

{
"id": 3,
"name": "es",
"description": "testdescput1"
}
#### Response
HTTP/1.1 400 Bad Request
Date: Sun, 02 Jan 2022 21:08:45 GMT
Content-Type: text/plain; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

Name should be at least 3 characters long

Response code: 400 (Bad Request); Time: 5ms; Content length: 41 bytes

#### Request
PUT https://localhost:5001/api/Softwares/3
Content-Type: application/json

{
"id": 4,
"name": "testcall1",
"description": "testdescput1"
}

#### Response
HTTP/1.1 400 Bad Request
Date: Sun, 02 Jan 2022 21:07:16 GMT
Content-Type: text/plain; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

Ids should match

Response code: 400 (Bad Request); Time: 28ms; Content length: 16 bytes

