import { useState, useEffect }  from 'react'
import { CardDisplayer, EventPreviewCard, JoinButton, SearchForm } from 'Components'
import LeaveButton from 'Pages/JoinedEvents/Components/LeaveButton'


export default function JoinedEvents() {
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
      <SearchForm isOpen={isOpen} setIsOpen={setIsOpen} url={searchUrl} setEvents={setEvents} searchType='applied'/>
      <CardDisplayer isOpen={isOpen} setIsOpen={setIsOpen} events={events} setEvents={setEvents}>
        <EventPreviewCard>
          <LeaveButton />
        </EventPreviewCard>
      </CardDisplayer>
    </>
  )
}
