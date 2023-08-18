import { useState, useEffect }  from 'react'

import { CardDisplayer, EventPreviewCard, SearchForm } from 'Components'
import DeleteButton from './Components/DeleteButton'
import UpdateButton from './Components/UpdateButton'


export default function CreatedEvents() {
  const [isOpen,setIsOpen] = useState(false)

  const [events, setEvents] = useState([])

  const searchUrl = 'http://localhost:5000/EventEndpoints/search'

  useEffect(()=>{
    fetch('http://localhost:5000/EventEndpoints/search?searchType=created',{credentials:'include'})
    .then(res=>res.json())
    .then(data=>{
      console.log(data.events);
      setEvents(data.events)
    })
    .catch(console.log)
  },[])

  return (
    <>
      <SearchForm isOpen={isOpen} setIsOpen={setIsOpen} url={searchUrl} setEvents={setEvents} searchType='created'/>
      <CardDisplayer isOpen={isOpen} setIsOpen={setIsOpen} events={events} setEvents={setEvents}>
        <EventPreviewCard>
          <DeleteButton />  
          <UpdateButton />
        </EventPreviewCard>
      </CardDisplayer>
    </>
  )
}
