import React from "react";
import Card from "../Card/Card";
import { CompanySearch } from "../../company";
import { v4 as uuidv4 } from "uuid";
import { PortfolioGet } from "../../Models/Portfolio";

interface Props {
  searchResults: CompanySearch[];
  onPortfolioCreate: (symbol: string) => void;
  portfolioValues: PortfolioGet[];
}

const CardList: React.FC<Props> = ({
  searchResults,
  onPortfolioCreate,
  portfolioValues,
}: Props): JSX.Element => {
  const filteredResults = searchResults.filter(
    (result) => result.symbol && !result.symbol.includes(".")
  );

  return (
    <div>
      {filteredResults.length > 0 ? (
        filteredResults.map((result) => {
          const exists = portfolioValues.some(
            (p) => p.symbol === result.symbol
          );

          return (
            <Card
              id={result.symbol}
              key={uuidv4()}
              searchResult={result}
              onPortfolioCreate={onPortfolioCreate}
              isInPortfolio={exists}
            />
          );
        })
      ) : (
        <p className="mb-3 mt-3 text-xl font-semibold text-center md:text-xl">
          No results!
        </p>
      )}
    </div>
  );
};

export default CardList;