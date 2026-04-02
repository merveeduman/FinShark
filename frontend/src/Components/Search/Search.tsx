import React, { ChangeEvent, JSX, SyntheticEvent, useState } from 'react'

type Props = {}

const Search :React.FC<Props> = (props: Props):JSX.Element => {
    const[search, setSearch] = useState<string>("");

    const handleChange = (e:ChangeEvent<HTMLInputElement>) => {
        setSearch(e.target.value);
        console.log(e);
    };

    const onClick =  (e:SyntheticEvent) => {
      console.log(e);
    };

  return (<div><input value={search} onChange={(e) => onClick(e)}></input></div>)
  
}

export default Search