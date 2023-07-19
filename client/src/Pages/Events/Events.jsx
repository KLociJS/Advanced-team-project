import React, { useState } from 'react'
import './Events.css'
import { DateInput, Input } from 'Components'

import { HiMiniMagnifyingGlass } from 'react-icons/hi2'

export default function Events() {

  const [isOpen,setIsOpen] = useState(false)

  const [eventName, setEventName] = useState('')
  const [location, setLocation] = useState('')
  const [distance, setDisctance] = useState('')
  const [category, setCategory] = useState('')
  const [minPrice,setMinPrice] = useState('')
  const [maxPrice, setMaxPrice] = useState('')
  const [startDate, setStartDate] = useState('')
  const [endDate, setEndDate] = useState('')

  const handleSearchSubmit = (e) =>{
    e.preventDefault()

    console.log(eventName,location,distance,category,minPrice,maxPrice,startDate, endDate)
  }

  return (
    <>
      <form className={`search-form${isOpen ? ' active' : ''}`} onSubmit={handleSearchSubmit}>
        <h1 className='center-text'>
          Search
          <HiMiniMagnifyingGlass className='react-icon'/> 
        </h1>
        <p className='mb-2 secondary-text'>Leaving a field empty doesnt filter by the given property.</p>
        <Input label='Event Name' inputValue={eventName} setInputValue={setEventName}/>
        <Input label='Location' inputValue={location} setInputValue={setLocation} />
        <Input label='Max Distance From Location' type='number' inputValue={distance} setInputValue={setDisctance}/>
        <Input label='Category' inputValue={category} setInputValue={setCategory} />
        <DateInput label='Starting Date' inputValue={startDate} setInputValue={setStartDate}/>
        <DateInput label='Ending Date' inputValue={endDate} setInputValue={setEndDate} />
        <div className='flex-container'>
          <Input label='Min Price' type='number' inputValue={minPrice} setInputValue={setMinPrice} />
          <Input label='Max Price' type='number' inputValue={maxPrice} setInputValue={setMaxPrice} />
        </div>
        <button onClick={()=>setIsOpen(false)}>Search</button>
      </form>
      <section className='card-container'>
      <button className={`search-button${isOpen ? ' hidden' : ''}`} onClick={()=>setIsOpen(b=>!b)}>Search</button>

      </section>
    </>
  )
}
