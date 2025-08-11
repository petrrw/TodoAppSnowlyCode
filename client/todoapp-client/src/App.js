import { useEffect } from 'react';
import './App.css';
import ToDoList from './Components/ToDoList';
import ApiClient from './Services/ApiClient';
import { useState } from 'react';

function App() {
  const [items, setItems] = useState([]);
  const api = new ApiClient(process.env.REACT_APP_BACKENDURL)

useEffect(() => {
  api.getAll().then(items => {
    setItems(items);
  }).catch(() => alert('Failed to fetch todos'));
}, [])

const handleDelete = id => {
  api.delete(id).then(() => {
       setItems(items.filter(item => item.id !== id));
  }).catch(()=> alert("Something went wrong during removing an item."))
}

const handleUpdate = (id, newValue) => {
  api.update(id, newValue).then(updatedItem => {
    setItems(items.map(item => item.id === updatedItem.id ? updatedItem : item));
  }).catch(() => alert("Something went wrong during updating an item."));
}

const handleAdd = (newItem) => {
  api.create(newItem).then(addedItem => {
    setItems([...items, addedItem]);
  }).catch(() => alert("Something went wrong during adding an item."));
}


  return (
    <div className="App">
      <header className="App-header">
        <ToDoList items={items} onDelete={handleDelete} onUpdate={handleUpdate} onAdd={handleAdd} />
      </header>
    </div>
  );
}

export default App;
