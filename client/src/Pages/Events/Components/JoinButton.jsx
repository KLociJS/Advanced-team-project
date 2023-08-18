import useAuth from 'Hooks/useAuth'
import PrimaryButton from '../../../Components/Button/PrimaryButton'

export default function JoinButton({eventId, setEvents}) {

    const { user } = useAuth()

    const eventJoinHandler = (id) => {
        fetch(`http://localhost:5000/EventEndpoints/join-event/${id}`,{
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

    if(!user) return null

    return <PrimaryButton text='Join event' clickHandler={()=>eventJoinHandler(eventId)}/>
  
}
