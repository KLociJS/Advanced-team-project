import { useState, useEffect } from 'react'
import { CardDisplayer, EventPreviewCard, SearchForm } from 'Components'
import  JoinButton  from './Components/JoinButton'

export default function Events() {

  const [isOpen,setIsOpen] = useState(false)

  const [events, setEvents] = useState([])

  const searchUrl = 'http://localhost:5000/EventEndpoints/search'

  useEffect(()=>{
    fetch('http://localhost:5000/EventEndpoints/search?searchType=all')
    .then(res=>res.json())
    .then(data=>{
      console.log(data.events);
      setEvents(data.events)
    })
    .catch(console.log)

    return ()=>{
      
    }
  },[])

  return (
    <>
      <SearchForm isOpen={isOpen} setIsOpen={setIsOpen} url={searchUrl} setEvents={setEvents} searchType='all'/>
      <CardDisplayer isOpen={isOpen} setIsOpen={setIsOpen} events={events} setEvents={setEvents}>
        <EventPreviewCard>
          <JoinButton />
        </EventPreviewCard>
      </CardDisplayer>
    </>
  )
}
