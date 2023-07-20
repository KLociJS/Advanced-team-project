import React from 'react'
import './CardDisplayer.css'

import { HiMiniMagnifyingGlass } from 'react-icons/hi2'

export default function CardDisplayer({ isOpen, setIsOpen }) {
  return (
    <section className='card-container'>
      <button className={`searchbar-toggle${isOpen ? ' hidden' : ''}`} onClick={()=>setIsOpen(b=>!b)}>
        Search
        <HiMiniMagnifyingGlass className='searchbar-toggle-icon'/>
      </button>
    </section>
  )
}
