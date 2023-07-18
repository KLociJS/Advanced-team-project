import React, { useState } from 'react'
import './Events.css'

export default function Events() {

  const [isOpen,setIsOpen] = useState(false)

  const handleSearchSubmit = (e) =>{
    e.preventDefault()
  }

  return (
    <>
      <form className={`search-form${isOpen ? ' active' : ''}`} onSubmit={handleSearchSubmit}>
        <button onClick={()=>setIsOpen(false)}>Search</button>
      </form>
      <section className='card-container'>
      <button className={`search-button${isOpen ? ' hidden' : ''}`} onClick={()=>setIsOpen(b=>!b)}>Search</button>

      </section>
    </>
  )
}
