import useAuth from 'Hooks/useAuth'


export default function AuthorizedRender({ children }) {
    const { user } = useAuth()
    
    if(!user){
        return null
    }

    return (
        <>
            {children}
        </>
    )
}
