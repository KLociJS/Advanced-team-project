import React, { useEffect, useState } from 'react'
import './CardDisplayer.css'

import { HiMiniMagnifyingGlass } from 'react-icons/hi2'
import { EventPreviewCard } from 'Components'

export default function CardDisplayer({ isOpen, setIsOpen, fetchUrl }) {
  const [events, setEvents] = useState([])

  useEffect(()=>{
    fetch(fetchUrl)
    .then(res=>res.json())
    .then(data=>{
      console.log(data.events);
      setEvents(data.events)
    })
    .catch(console.log)
  },[fetchUrl])

  return (
    <section className='card-container'>
      <div className='search-button-container'>
        <button className={`searchbar-toggle${isOpen ? ' hidden' : ''}`} onClick={()=>setIsOpen(b=>!b)}>
          Search
        </button>
      </div>
        <HiMiniMagnifyingGlass className='searchbar-toggle-icon'/>
        {events.map(e=>(
          <EventPreviewCard 
            name={e.eventName}
            location={e.location.name}
            category={e.category.name}
            price={e.price}
            recommendedAge={e.recommendedAge}
            startingDate={e.startingDate}
            endingDate={e.endingDate}
          />
        ))}
    </section>
  )
}
