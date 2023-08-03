import React from 'react'
import './EventPreviewCard.css'

import { CiLocationOn } from 'react-icons/ci'
import { PiBabyLight } from 'react-icons/pi'
import { BsCashCoin } from 'react-icons/bs'
import { CiCalendar } from 'react-icons/ci'

export default function EventPreviewCard({ event, setEvents, children }) {
    return (
        <section className='event-card'>
            <h1 className='mb-1'>{event.eventName}</h1>
            <h3>{event.description}</h3>
            <h3>
                <CiLocationOn className='card-icon' />
                {event.location.name}
            </h3>
            <h3>
                {event.category.name}
            </h3>
            <p>
                <PiBabyLight className='card-icon' />
                {event.recommendedAge}+
            </p>
            <p>
                <BsCashCoin className='card-icon' />
                {event.price > 0 ? `${event.price} HUF` : 'Free'}
            </p>
            <div className='date-icon-container'>
                <CiCalendar className='card-icon' />
                <div>
                    <p>{event.startingDate.substring(0, 10)} {event.startingDate.substring(11, 16)}</p>
                    <p>{event.endingDate.substring(0, 10)} {event.endingDate.substring(11, 16)}</p>
                </div>
            </div>
            <div>
                {React.Children.map(children, (child) => {
                    return React.cloneElement(child, {
                        eventId: event.id,
                        setEvents: setEvents,
                    });
                })}
            </div>
        </section>
    )
}
