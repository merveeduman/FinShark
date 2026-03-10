import React, { JSX } from 'react'
import Card from '../Card/Card'

type Props = {}

const CardList: React.FC<Props> = (props: Props) : JSX.Element => {
  return (
    <div>
      <Card companyName='Apple' ticker='AAPL' price={110.0} />
      <Card companyName='Microsoft' ticker='MSFT' price={200.0} />
      <Card companyName='Tesla' ticker='TSLA' price={250.0} />
    </div>
  )
}

export default CardList