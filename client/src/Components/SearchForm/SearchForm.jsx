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
    const [distance, setDisctance] = useState('')
    const [category, setCategory] = useState('')
    const [minPrice,setMinPrice] = useState('')
    const [maxPrice, setMaxPrice] = useState('')
    const [startDate, setStartDate] = useState('')
    const [startTime, setStartTime] = useState('00:00')
    const [endDate, setEndDate] = useState('')
    const [endTime, setEndTime] = useState('00:00')

    //dictionary with params, iterating throught and concatenating url

    const handleSearchSubmit = (e) =>{
        e.preventDefault()
        setIsOpen(false)

        const startDateTime = startDate ? new Date(startDate).toISOString().replace(/\d{2}:\d{2}/g,startTime) : ''
        const endDateTime = endDate ? new Date(endDate).toISOString().replace(/\d{2}:\d{2}/g,endTime) : ''

        const searchParamsDict = {
            eventName ,
            location,
            distance,
            category,
            minPrice,
            maxPrice,
            startingDate: startDateTime,
            endingDate: endDateTime
        }

        let searchParams = ''
        Object.keys(searchParamsDict).forEach(key=>{
            if(searchParamsDict[key]!==''){
                if(searchParams === ''){
                    searchParams = `?${key}=${searchParamsDict[key]}`
                }else{
                    searchParams += `&${key}=${searchParamsDict[key]}`
                }
            }
        })

        console.log(searchParams);
        
        fetch(`${url}${searchParams}`)
        .then(res=>res.json())
        .then(data=>{
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
                url={locationsUrl}
            />
            <Input label='Max Distance From Location' type='number' inputValue={distance} setInputValue={setDisctance}/>
            <AutoCompleteInput 
                label='Category'
                inputValue={category}
                setInputValue={setCategory}
                url={categoriesUrl}
            />
            <DateInput label='Starting Date' dateValue={startDate} setDateValue={setStartDate} time={startTime} setTime={setStartTime}/>
            <DateInput label='Ending Date' dateValue={endDate} setDateValue={setEndDate} time={endTime} setTime={setEndTime}/>
            <div className='flex-container mb-1'>
                <Input label='Min Price' type='number' inputValue={minPrice} setInputValue={setMinPrice} />
                <Input label='Max Price' type='number' inputValue={maxPrice} setInputValue={setMaxPrice} />
            </div>
            <PrimaryButton text='Search' clickHandler={handleSearchSubmit}/>
            <SecondaryButton text='Cancel' clickHandler={()=>setIsOpen(false)} />
        </form>
    )
}
