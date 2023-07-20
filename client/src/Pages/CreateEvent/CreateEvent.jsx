import { DateInput, Input, PrimaryButton, SecondaryButton } from 'Components'
import React from 'react'

export default function CreateEvent() {

  const clickHandler = {

  }

  return (
    <div className='event-form'>
      <h1>Create New Event</h1>
      <Input label='Event name'/>
      <Input label='Category'/>
      <Input label='Location'/>
      <Input label='Price'/>
      <Input label='Max Headcount'/>
      <Input label='Recommended Age'/>
      <DateInput label='Startin Date'/>
      <DateInput label='Ending Date'/>
      <PrimaryButton text='Create' clickHandler={clickHandler}/>
      <SecondaryButton text='Cancel' />
    </div>
  )
}
