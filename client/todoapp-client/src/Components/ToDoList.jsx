import { useState } from "react";
import ToDoItem from "./ToDoItem";

const ToDoList = ({items, onDelete, onUpdate, onAdd}) => {
  const [inAddMode, setInAddMode] = useState(false)

  const handleAdd = (newItem) => {
    onAdd(newItem);
    setInAddMode(false);
  }

  return (
  <div>
    <h2>ToDo list</h2>
    <div className="container d-flex gap-2 flex-wrap">

      { inAddMode ?
          <ToDoItem
            id={""}
            title={""}
            createdAt={null}
            dueDate={null}
            isCompleted={false}
            onDelete={onDelete}
            onUpdate={onUpdate}
            addMode={true}
            onAdd={handleAdd}
          />
          :
        <button onClick={() => setInAddMode(true)} className="btn btn-primary btn-sm">
          Add Task
        </button>
      }

       {inAddMode
       ? null
       : items.length > 0
       ? items.map(item => (
       <ToDoItem
          key={item.id}
          id={item.id}
          title={item.title}
          createdAt={item.createdAt}
          dueDate={item.dueDate}
          isCompleted={item.isCompleted}
          onDelete={onDelete}
          onUpdate={onUpdate}
          />
        ))
        : <h1>No tasks available</h1>
    }
    </div>
  </div>
  );
};

export default ToDoList;
