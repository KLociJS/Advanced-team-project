import { useState } from 'react'
import './Events.css'
import { SearchForm } from 'Components'

import { HiMiniMagnifyingGlass } from 'react-icons/hi2'


export default function Events() {

  const [isOpen,setIsOpen] = useState(false)

  return (
    <>
      <SearchForm isOpen={isOpen} setIsOpen={setIsOpen} />
      <section className='card-container'>
      <button className={`searchbar-toggle${isOpen ? ' hidden' : ''}`} onClick={()=>setIsOpen(b=>!b)}>
        Search
        <HiMiniMagnifyingGlass className='searchbar-toggle-icon'/>
      </button>

      </section>
    </>
  )
}
