## Intro

This assignment is a task to create a Charity Donation Tax Reduction calculator. The reduction is what the donator
will receive back from his taxes at the end of the year. 

For reference, the Donation Tax Return is calculated as follows:
`[Donation Amount] * ([TaxRate] / (100 - [TaxRate]))` 

- The assignment should take on average about 30-60 minutes.
- We use [MSTest](https://docs.microsoft.com/en-us/visualstudio/test/unit-test-basics) and [FakeItEasy](https://fakeiteasy.github.io/), references have been added using NuGet Packages. Everything is in place for you to just write the code (no "File > New Project" required).

## Task requirements

- All stories to be completed with an appropriate level of testing.
- No actual database implementation is required, feel free to stub it out.
- Your code should trend towards being SOLID.
- Send us a zip or a link (DropBox/OneDrive/whatever) to your zipped code.

## Task Stories

### Story 1

As a **donor**  
I want **to see my Donation Tax Return calculated according to the current tax rate**  
So that **I know how much tax I will pay less**

#### Acceptance criteria

- Donation Tax Return calculated at a tax rate of 20%.
- Supported by unit tests.

---

### Story 2

As a **site administrator**  
I want **to be able to change the applicable tax rate**  
So that **I don't need to change the code when the tax rate changes**

#### Acceptance criteria

- Current Tax Rate is retrieved from data store.
- Donation Tax Return amount is calculated based on the current amount in the data store.

---

### Story 3

As a **donor**  
I want **to see my Donation Tax Return amount rounded correctly to 2 decimal places**  
So that **I'm not confused about how much will be returned**

#### Acceptance criteria

- Donation Tax Return amount correctly rounded to 2 decimal places (1.316 should round to 1.32).

---

### Story 4

As a **tax revenue service**  
I want **to increase the Donation Tax Return based on the donation type**  
So that **people will feel inspired to donate to these donation types**

#### Acceptance criteria

- 5% tax return increase added for donations to "HumanRights" charities.
- 3% supplement added for donations to "Environmental" charities.
- No supplement should be applied for other charities.

---

### Story 5

As a **tax revenue service**
I want **to cap the amount of the Donation Tax Return to 1000 EUR**
So that **the amount returned does not rise higher than a maximum**

#### Acceptance criteria

- The Donation Tax Return amount is never higher than 1000 EUR

---