import { AutoCompleteInput, DateInput, Input, PrimaryButton, SecondaryButton } from 'Components'
import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom'

export default function EventForm({url,navigateTo,buttonText,event, method}) {
    const locationsUrl = "http://localhost:5000/EventEndpoints/location?location="
    const categoriesUrl = "http://localhost:5000/EventEndpoints/category?category="
  
    const navigate = useNavigate()
  
    const [eventName, setEventName] = useState(event?.eventName || '')
    const [description, setDescription] = useState(event?.description || '')
    const [category, setCategory] = useState(event?.category.name || '')
    const [location, setLocation] = useState(event?.location.name || '')
    const [price, setPrice] = useState(event?.price || '')
    const [maxHeadCount, setMaxHeadCount] = useState(event?.headCount || '')
    const [recommendedAge, setRecommendedAge] = useState( event?.recommendedAge || '')
    const [startingDate, setStartingDate] = useState(event?.startingDate.substring(0,10) || '')
    const startingDateTime = event?.startingDate.substring(11,16)
    const [startingTime, setStartingTime] = useState(startingDateTime || '00:00')
    const [endingDate, setEndingDate] = useState(event?.endingDate.substring(0,10) || '')
    const endingDateTime = event?.endingDate.substring(11,16)
    const [endingTime, setEndingTime] = useState(endingDateTime || '00:00')
  
    const clickHandler = (e) => {
      e.preventDefault()
  
      const startingDateTime = new Date(startingDate).toISOString().replace(/\d{2}:\d{2}/g,startingTime)
      const endingDateTime = new Date(endingDate).toISOString().replace(/\d{2}:\d{2}/g,endingTime)
  
      const newEvent = {
        category,
        location,
        description,
        eventName,
        startingDate : startingDateTime,
        endingDate: endingDateTime,
        headcount : maxHeadCount,
        price,
        recommendedAge
      }
  
      console.log(newEvent);
  
      fetch(url,{
        method: method,
        headers: {
          'content-type' : 'application/json'
        },
        credentials: "include",
        body: JSON.stringify(newEvent)
      })
      .then(res=>res.json())
      .then(data=>{
        console.log(data)
        navigate(navigateTo)
      })
      .catch(console.log)
      
    }
  
    return (
      <div className='event-form'>
        <h1>Create New Event</h1>
        <Input label='Event name' inputValue={eventName} setInputValue={setEventName}/>
        <Input label='Description' inputValue={description} setInputValue={setDescription} />
        <AutoCompleteInput 
          label='Category'
          inputValue={category}
          setInputValue={setCategory}
          url={categoriesUrl}
        />
        <AutoCompleteInput 
          label='Location'
          inputValue={location}
          setInputValue={setLocation}
          url={locationsUrl}
        />
        <Input label='Price' type='number' inputValue={price} setInputValue={setPrice}/>
        <Input label='Max Headcount' type='number' inputValue={maxHeadCount} setInputValue={setMaxHeadCount}/>
        <Input label='Recommended Age' type='number' inputValue={recommendedAge} setInputValue={setRecommendedAge}/>
        <DateInput label='Startin Date' dateValue={startingDate} setDateValue={setStartingDate} time={startingTime} setTime={setStartingTime}/>
        <DateInput label='Ending Date' dateValue={endingDate} setDateValue={setEndingDate} time={endingTime} setTime={setEndingTime}/>
        <PrimaryButton text={buttonText} clickHandler={clickHandler} />
        <SecondaryButton text='Cancel' />
      </div>
    )
}
