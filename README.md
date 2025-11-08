# CourierApp

A clean, SOLID .NET 6+ console app for sorting courier packages by discounted delivery cost.

## Build

```sh
dotnet build

Run

dotnet run --project CourierApp -- [input.txt]

To use BubbleSorter:

dotnet run --project CourierApp -- --sorter bubble [input.txt]

Input Format
Each line: <PackageId> <Weight> <Distance> <OfferCode>

Example:

PKG1 100 100 OFR002
PKG2 50 50 OFR003
PKG3 300 300 OFR001


Output
Each line: <PackageId> <DiscountedCost> <DiscountApplied>

Example:

PKG2 950.00 50.00
PKG1 930.00 70.00
PKG3 1000.00 0.00

Test
dotnet test
