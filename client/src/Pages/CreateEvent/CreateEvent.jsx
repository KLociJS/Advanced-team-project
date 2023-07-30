import { AutoCompleteInput, DateInput, Input, PrimaryButton, SecondaryButton } from 'Components'
import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom'

export default function CreateEvent() {

  const locationsUrl = "https://localhost:7019/EventEndpoints/location?location="
  const categoriesUrl = "https://localhost:7019/EventEndpoints/category?category="

  const navigate = useNavigate()

  const [eventName, setEventName] = useState('')
  const [category, setCategory] = useState('')
  const [categoryId, setCategoryId] = useState('')
  const [location, setLocation] = useState('')
  const [locationId, setLocationId] = useState('')
  const [price, setPrice] = useState('')
  const [maxHeadCount, setMaxHeadCount] = useState('')
  const [recommendedAge, setRecommendedAge] = useState('')
  const [startingDate, setStartingDate] = useState('')
  const [endingDate, setEndingDate] = useState('')

  const clickHandler = (e) => {
    e.preventDefault()

    const newEvent = {
      categoryId,
      locationId,
      eventName,
      startingDate,
      endingDate,
      headcount : maxHeadCount,
      price,
      recommendedAge,
      userID: '1'
    }

    console.log(newEvent);

    fetch('https://localhost:7019/EventEndpoints',{
      method: 'POST',
      headers: {
        'content-type' : 'application/json'
      },
      body: JSON.stringify(newEvent)
    })
    .then(res=>res.json())
    .then(data=>{
      console.log(data)
      navigate('/events')
    })
    .catch(console.log)
    
  }

  return (
    <div className='event-form'>
      <h1>Create New Event</h1>
      <Input label='Event name' inputValue={eventName} setInputValue={setEventName}/>
      <AutoCompleteInput 
        label='Category'
        inputValue={category}
        setInputValue={setCategory}
        setId={setCategoryId}
        url={categoriesUrl}
      />
      <AutoCompleteInput 
        label='Location'
        inputValue={location}
        setInputValue={setLocation}
        setId={setLocationId}
        url={locationsUrl}
      />
      <Input label='Price' type='number' inputValue={price} setInputValue={setPrice}/>
      <Input label='Max Headcount' type='number' inputValue={maxHeadCount} setInputValue={setMaxHeadCount}/>
      <Input label='Recommended Age' type='number' inputValue={recommendedAge} setInputValue={setRecommendedAge}/>
      <DateInput label='Startin Date' inputValue={startingDate} setInputValue={setStartingDate}/>
      <DateInput label='Ending Date' inputValue={endingDate} setInputValue={setEndingDate}/>
      <PrimaryButton text='Create' clickHandler={clickHandler} />
      <SecondaryButton text='Cancel' />
    </div>
  )
}
