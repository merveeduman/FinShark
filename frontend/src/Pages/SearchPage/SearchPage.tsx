import React, { useState, ChangeEvent, SyntheticEvent, useEffect } from "react";
import { CompanySearch } from "../../company";
import { searchCompanies } from "../../api";
import Search from "../../Components/Search/Search";
import ListPortfolio from "../../Components/Portfolio/ListPortfolio/ListPortfolio";
import CardList from "../../Components/CardList/CardList";
import { PortfolioGet } from "../../Models/Portfolio";
import {
  portfolioAddAPI,
  portfolioDeleteAPI,
  portfolioGetAPI,
} from "../../Services/PortfolioService";
import { toast } from "react-toastify";

interface Props {}

const SearchPage = (props: Props) => {
  const [search, setSearch] = useState<string>("");
  const [portfolioValues, setPortfolioValues] = useState<PortfolioGet[] | null>(
    []
  );
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([]);
  const [serverError, setServerError] = useState<string | null>(null);
  const [hasSearched, setHasSearched] = useState(false);

  useEffect(() => {
    getPortfolio();
  }, []);

  const handleSearchChange = async (e: ChangeEvent<HTMLInputElement>) => {
  const value = e.target.value;
  setSearch(value);

  if (value.trim() === "") {
    setSearchResult([]);
    return;
  }

  const result = await searchCompanies(value);

  if (typeof result === "string") {
    setServerError(result);
    setSearchResult([]);
  } else if (Array.isArray(result)) {
    setSearchResult(result);
    setServerError(null);
  }
};

  const getPortfolio = () => {
    portfolioGetAPI()
      .then((res) => {
        if (res?.data) {
          setPortfolioValues(res.data);
        }
      })
      .catch(() => {
        setPortfolioValues(null);
      });
  };

  const onPortfolioCreate = (e: any) => {
    e.preventDefault();
    portfolioAddAPI(e.target[0].value)
      .then((res) => {
        if (res?.status === 204) {
          toast.success("Stock added to portfolio!");
          getPortfolio();
        }
      })
      .catch(() => {
        toast.warning("Could not add stock to portfolio!");
      });
  };

  const onPortfolioDelete = (e: any) => {
    e.preventDefault();
    portfolioDeleteAPI(e.target[0].value).then((res) => {
      if (res?.status === 200) {
        toast.success("Stock deleted from portfolio!");
        getPortfolio();
      }
    });
  };

  const onSearchSubmit = async (e: SyntheticEvent) => {
    e.preventDefault();

    if (!search.trim()) {
      setSearchResult([]);
      setHasSearched(false);
      return;
    }

    setHasSearched(true);

    const result = await searchCompanies(search.trim());
    console.log("SEARCH RESULT:", result);

    if (typeof result === "string") {
      setServerError(result);
      setSearchResult([]);
    } else if (Array.isArray(result)) {
      setSearchResult(result);
      setServerError(null);
    }
  };

  return (
    <>
      <Search
        onSearchSubmit={onSearchSubmit}
        search={search}
        handleSearchChange={handleSearchChange}
      />

      <ListPortfolio
        portfolioValues={portfolioValues!}
        onPortfolioDelete={onPortfolioDelete}
      />

      {serverError && <div>Unable to connect to API</div>}

      {hasSearched && (
        <CardList
          searchResults={searchResult}
          onPortfolioCreate={onPortfolioCreate}
        />
      )}
    </>
  );
};

export default SearchPage;