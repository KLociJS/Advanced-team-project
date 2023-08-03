import React from 'react'
import './CardDisplayer.css'

import { HiMiniMagnifyingGlass } from 'react-icons/hi2'

export default function CardDisplayer({ isOpen, setIsOpen, events, setEvents, children }) {

  return (
    <section className='card-container'>
      <div className='search-button-container'>
        <button className={`searchbar-toggle${isOpen ? ' hidden' : ''}`} onClick={()=>setIsOpen(b=>!b)}>
          Search
        <HiMiniMagnifyingGlass className='searchbar-toggle-icon'/>
        </button>
      </div>
      {events.map((e) => {
        return React.cloneElement(children, {
          key: e.id,
          event: e,
          setEvents: setEvents
        });
      })}
    </section>
  )
}
