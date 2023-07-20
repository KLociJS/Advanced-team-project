import { useState } from 'react'

export default function AutoCompleteInput({ inputValue, setInputValue, setId, label, type, url }) {

    const [suggestions, setSuggestions] = useState([])

    const [isFocused, setIsFocused] = useState(false)

    const handleFocus = () => setIsFocused(true)
    const handleBlur = () => setIsFocused(false)
  
    const isActive = isFocused || inputValue ? 'active' : ''

    console.log(suggestions);

    const handleInputChange = async(e) => {
        const value = e.target.value
        setInputValue(value)

        if(value.length>2){
            const currSuggestions = await fetchSuggestions(url+value)
            console.log(currSuggestions);
            setSuggestions(currSuggestions.data)

            const isLoactionMatches = currSuggestions.data.find(l=>l.name===value)
            if(isLoactionMatches){
                console.log('c');
                const suggestionId = currSuggestions.data.find(s=>s.name===value).id;
                setId(suggestionId)
            }
        }
    }

    const fetchSuggestions = async (url) =>{
        const suggestionsResponse = await fetch(url)
        return await suggestionsResponse.json()
    }
  
    return (
          <div className={`input-group`}>
              <label className={`input-label ${isActive}`}>
                  {label}
              </label>
              <input
                  type={type || 'text'}
                  value={inputValue}
                  className="input-control"
                  onFocus={handleFocus}
                  onBlur={handleBlur}
                  onChange={handleInputChange}
                  autoComplete="off"
                  list={label}
              />
              <datalist id={label}>
                {suggestions.map(s=>(
                    <option key={s.id} value={s.name}> {s.name} </option>
                ))}
              </datalist>
      </div>
    );
}
