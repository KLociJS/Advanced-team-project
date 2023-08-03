import PrimaryButton from '../../../Components/Button/PrimaryButton'


export default function LeaveButton({eventId, setEvents}) {
    console.log(eventId)

    const eventLeaveHandler = (id) => {
        fetch(`https://localhost:7019/EventEndpoints/leave-event/${id}`,{
            method: 'PATCH',
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
  return <PrimaryButton text='Leave event' clickHandler={()=>eventLeaveHandler(eventId)}/>
  
}