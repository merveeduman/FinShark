import { SyntheticEvent } from "react";

interface Props {
  onPortfolioCreate: (symbol: string) => void;
  symbol: string;
}

const AddPortfolio = ({ onPortfolioCreate, symbol }: Props) => {
  return (
    <div className="flex flex-col items-center justify-end flex-1 space-x-4 space-y-2 md:flex-row md:space-y-0">
      <button
        type="button"
        onClick={() => onPortfolioCreate(symbol)}
        className="p-2 px-8 text-white bg-darkBlue rounded-lg hover:opacity-70 focus:outline-none"
      >
        Add
      </button>
    </div>
  );
};

export default AddPortfolio;
