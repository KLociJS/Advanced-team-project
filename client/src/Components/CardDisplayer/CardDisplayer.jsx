import React, { useEffect, useState } from 'react'
import './CardDisplayer.css'

import { HiMiniMagnifyingGlass } from 'react-icons/hi2'
import { EventPreviewCard } from 'Components'

export default function CardDisplayer({ isOpen, setIsOpen, events, setEvents }) {

  return (
    <section className='card-container'>
      <div className='search-button-container'>
        <button className={`searchbar-toggle${isOpen ? ' hidden' : ''}`} onClick={()=>setIsOpen(b=>!b)}>
          Search
        <HiMiniMagnifyingGlass className='searchbar-toggle-icon'/>
        </button>
      </div>
        {events.map(e=>(
          <EventPreviewCard
            key={e.id}
            name={e.eventName}
            location={e.location.name}
            category={e.category.name}
            price={e.price}
            recommendedAge={e.recommendedAge}
            startingDate={e.startingDate}
            endingDate={e.endingDate}
            description={e.description}
            id={e.id}
            setEvents={setEvents}
          />
        ))}
    </section>
  )
}
