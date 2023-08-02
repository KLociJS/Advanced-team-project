import { useState, useEffect } from 'react'
import { CardDisplayer, SearchForm } from 'Components'

export default function Events() {

  const [isOpen,setIsOpen] = useState(false)

  const [events, setEvents] = useState([])

  const searchUrl = 'https://localhost:7019/EventEndpoints/search'

  useEffect(()=>{
    fetch('https://localhost:7019/EventEndpoints')
    .then(res=>res.json())
    .then(data=>{
      console.log(data.events);
      setEvents(data.events)
    })
    .catch(console.log)
  },[])

  return (
    <>
      <SearchForm isOpen={isOpen} setIsOpen={setIsOpen} url={searchUrl} setEvents={setEvents}/>
      <CardDisplayer isOpen={isOpen} setIsOpen={setIsOpen} events={events} setEvents={setEvents}/>
    </>
  )
}
