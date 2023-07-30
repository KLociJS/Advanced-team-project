import React, { useState } from 'react'
import './SearchForm.css'

import { HiMiniMagnifyingGlass } from 'react-icons/hi2'
import { 
  PrimaryButton,
  SecondaryButton,
  DateInput, 
  Input, 
  AutoCompleteInput,
 } from 'Components'



export default function SearchForm({isOpen, setIsOpen, url, setEvents}) {

    const locationsUrl = "https://localhost:7019/EventEndpoints/location?location="
    const categoriesUrl = "https://localhost:7019/EventEndpoints/category?category="

    const [eventName, setEventName] = useState('')
    const [location, setLocation] = useState('')
    const [locationId,setLocationId] = useState()
    const [distance, setDisctance] = useState('')
    const [category, setCategory] = useState('')
    const [categoryId, setCategoryId] = useState()
    const [minPrice,setMinPrice] = useState('')
    const [maxPrice, setMaxPrice] = useState('')
    const [startDate, setStartDate] = useState('')
    const [endDate, setEndDate] = useState('')

    //dictionary with params, iterating throught and concatenating url

    const handleSearchSubmit = (e) =>{
        e.preventDefault()
        setIsOpen(false)

        const eventNameParam = eventName ? `?eventName=${eventName}` : ''
        const locationParam = location ? `&location=${location}` : ''
        const categoryParam = category ? `&category=${category}` : ''
        const minPriceParam = minPrice ? `&minPrice=${minPrice}` : ''
        const maxPriceParam = maxPrice ? `&maxPrice=${maxPrice}` : ''
        const startDateParam = startDate ? `&startingDate=${startDate}` : ''
        const endDateParam = endDate ? `&endingDate=${endDate}` : ''

        console.log(eventName,location,distance,category,minPrice,maxPrice,startDate, endDate)
        fetch(`${url}${eventNameParam}${startDateParam}${locationParam}${categoryParam}${minPriceParam}${maxPriceParam}${endDateParam}`)
        .then(res=>res.json())
        .then(data=>{
        console.log(data.events);
        setEvents(data.events)
        })
        .catch(console.log)
    }

    return (
        <form className={`search-form${isOpen ? ' active' : ''}`} onSubmit={handleSearchSubmit}>
            <h1 className='center-text'>
            Search
            <HiMiniMagnifyingGlass className='react-icon'/> 
            </h1>
            <p className='mb-2 secondary-text'>Leaving a field empty doesnt filter by the given property.</p>
            <Input label='Event Name' inputValue={eventName} setInputValue={setEventName}/>
            <AutoCompleteInput 
            label='Location'
            inputValue={location}
            setInputValue={setLocation}
            setId={setLocationId}
            url={locationsUrl}
            />
            <Input label='Max Distance From Location' type='number' inputValue={distance} setInputValue={setDisctance}/>
            <AutoCompleteInput 
            label='Category'
            inputValue={category}
            setInputValue={setCategory}
            setId={setCategoryId}
            url={categoriesUrl}
            />
            <DateInput label='Starting Date' inputValue={startDate} setInputValue={setStartDate}/>
            <DateInput label='Ending Date' inputValue={endDate} setInputValue={setEndDate} />
            <div className='flex-container mb-1'>
            <Input label='Min Price' type='number' inputValue={minPrice} setInputValue={setMinPrice} />
            <Input label='Max Price' type='number' inputValue={maxPrice} setInputValue={setMaxPrice} />
            </div>
            <PrimaryButton text='Search' clickHandler={handleSearchSubmit}/>
            <SecondaryButton text='Cancel' clickHandler={()=>setIsOpen(false)} />
        </form>
    )
}
