import { useState } from 'react'
import { CardDisplayer, SearchForm } from 'Components'

export default function Events() {

  const [isOpen,setIsOpen] = useState(false)
  const eventsFetchUrl = 'https://localhost:7019/EventEndpoints/search'

  return (
    <>
      <SearchForm isOpen={isOpen} setIsOpen={setIsOpen} />
      <CardDisplayer isOpen={isOpen} setIsOpen={setIsOpen} fetchUrl={eventsFetchUrl} />
    </>
  )
}
