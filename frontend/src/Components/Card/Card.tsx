import React, { JSX } from 'react'

interface Props {
    companyName: string;
    ticker: string;
    price: number;
}

const Card: React.FC<Props> = ({companyName, ticker, price}: Props): JSX.Element => {
  return (
    <div className="card">
      <img src="https://store.storeimages.cdn-apple.com/1/as-images.apple.com/is/iphone-get-ready-iphone-17-pro-hero-202509_FMT_WHH?wid=400&hei=504&fmt=png-alpha&.v=WTRHWFFSY3AwTk1OeS9CNDRPUG1OYVdxRlgwR2dSamhqMnhGamJuMDFDd2NFRFptUmRzSkV4ZmZYSEc2MTVxeUVVU0txUTdPMm5MVmUzc1FoRVJnN243WTg3a2d6Vmo0Sk9xZ0RhZEI1dWdTNitjbXhJK0hsNWpLeFJKVldHV3BsWG55WFNZREIrcHRwdlRvMGw2S3BR" alt="Image" />
      <div className="details">
        <h2>{companyName} ({ticker})</h2>
        <p>${price}</p>
      </div>
      <p className="info">Lorem ipsum dolor, sit amet consectetur adipisicing elit. Quaerat, inventore.
      </p>
    </div>
  )
}

export default Card