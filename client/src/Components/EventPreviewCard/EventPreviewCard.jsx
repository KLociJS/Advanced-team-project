import React from 'react'
import './EventPreviewCard.css'

import { CiLocationOn } from 'react-icons/ci'
import { PiBabyLight } from 'react-icons/pi'
import { BsCashCoin } from 'react-icons/bs'
import { CiCalendar } from 'react-icons/ci'
import { AuthorizedRender, PrimaryButton } from 'Components'

export default function EventPreviewCard(
    {   name,
        category,
        endingDate,
        startingDate,
        location,
        price,
        recommendedAge,
        description,
        id,
        setEvents
    })
{
    const eventJoinHandler = () => {
        fetch(`https://localhost:7019/EventEndpoints/join-event/${id}`,{
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        })
        .then(res=>{
            if(res.ok){
                setEvents(events=>events.filter(e=>e.id!==id))
            }
            return res.json()
        })
        .then(data=>{
            console.log(data)
        })
        .catch(console.log)
    }

    return (
        <section className='event-card'>
            <h1 className='mb-1'>{name}</h1>
            <h3>{description}</h3>
            <h3>
                <CiLocationOn className='card-icon' />
                {location}
            </h3>
            <h3>
                {category}
            </h3>
            <p>
                <PiBabyLight className='card-icon' />
                {recommendedAge}+
            </p>
            <p>
                <BsCashCoin className='card-icon'/>
                {price>0 ? `${price} HUF` : 'Free'}
            </p>
            <div className='date-icon-container'>
                <CiCalendar className='card-icon'/>
                <div>
                    <p>{startingDate.substring(0,10)} {startingDate.substring(11,16)}</p>
                    <p>{endingDate.substring(0,10)} {endingDate.substring(11,16)}</p>
                </div>
            </div>
            <AuthorizedRender>
                <PrimaryButton text='Join event' clickHandler={eventJoinHandler}/>
            </AuthorizedRender>
        </section>
    )
}
