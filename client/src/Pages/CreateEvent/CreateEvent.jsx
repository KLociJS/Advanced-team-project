import { AutoCompleteInput, DateInput, Input, PrimaryButton, SecondaryButton } from 'Components'
import React, { useState } from 'react'

export default function CreateEvent() {

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
    console.log(eventName,category,location,price,maxHeadCount,recommendedAge,startingDate,endingDate);
  }

  return (
    <div className='event-form'>
      <h1>Create New Event</h1>
      <AutoCompleteInput 
        label='category'
        inputValue={category}
        setInputValue={setCategory}
        setId={setCategoryId}
      />
      <Input label='Event name' inputValue={eventName} setInputValue={setEventName}/>
      <Input label='Category' inputValue={category} setInputValue={setCategory}/>
      <Input label='Location' inputValue={location} setInputValue={setLocation}/>
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
