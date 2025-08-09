import { useState } from "react";

const ToDoItem = ({ id, title, createdAt, dueDate, isCompleted, onDelete, onUpdate, addMode, onAdd }) => {
   const toDateInputValue = (date) => date ? new Date(date).toISOString().split("T")[0] : "";


  const [inEdit, setInEdit] = useState(false);
  const [actualTitle, setActualTitle] = useState(title);
  const [actualDueDate, setActualDueDate] = useState(toDateInputValue(dueDate));
  const [actualIsCompleted, setActualIsCompleted] = useState(isCompleted);

  const handleUpdate = () => {
    onUpdate(id, {
      title: actualTitle,
      dueDate: actualDueDate,
      isCompleted: actualIsCompleted,
    });
  };

  const handleAdd = () => {
    onAdd({
      title: actualTitle,
      dueDate: actualDueDate,
      isCompleted: actualIsCompleted,
    });
  };


  return (
    <div
      className="card mb-2 shadow-sm"
      style={{ maxWidth: "350px", borderRadius: "10px", fontSize: "0.9rem" }}
    >
      <div className="card-body py-2 px-3">
        <div className="mb-1 text-muted">
          <strong>ID:</strong> <span>{id}</span>
        </div>

        {inEdit || addMode ? (
          <>
            <div className="mb-1">
              <label htmlFor="title">Title:</label>
              <input
                name="title"
                type="text"
                className="form-control form-control-sm"
                value={actualTitle}
                onChange={(e) => setActualTitle(e.target.value)}
              />
            </div>

            <div className="mb-1">  
              <label htmlFor="dueDate">Due date:</label>
              <input
                name="dueDate"
                type="date"
                className="form-control form-control-sm"
                value={actualDueDate}
                onChange={(e) => setActualDueDate(e.target.value)}
              />
            </div>

            <div className="mb-1">
              <label htmlFor="isCompleted">IsCompleted:</label>
              <input
                name="isCompleted"
                type="checkbox"
                className="form-check-input ms-2"
                checked={actualIsCompleted}
                onChange={(e) => setActualIsCompleted(e.target.checked)}
              />
            </div>
          </>
        ) : (
          <>
            <div className="mb-1 text-muted">
              <strong>Title:</strong> <span>{title}</span>
            </div>
            <div className="mb-1 text-muted" style={{ fontSize: "0.85rem" }}>
              <strong>Created at:</strong> <span>{createdAt}</span>
            </div>
          
            <div className="text-muted" style={{ fontSize: "0.85rem" }}>
              <strong>Due date:</strong> <span>{dueDate ?? "none"}</span>
            </div>

            <div className="mb-1 text-muted" style={{ fontSize: "0.85rem" }}>
              <strong>IsCompleted:</strong>{" "}
              <span>{isCompleted ? "Yes" : "No"}</span>
            </div>
          </>
        )}

        <div className="mt-2">
          {inEdit || addMode ? (
            <>
              <button onClick={inEdit ? handleUpdate : handleAdd} className="btn btn-success btn-sm ms-2">
                Uložit
              </button>
                {  inEdit &&    
                <button
                onClick={() => setInEdit(false)}
                className="btn btn-secondary btn-sm ms-2"
              >
                Zrušit
              </button>}
            </>
          ) : (
            <>
              <button onClick={() => onDelete(id)} className="btn btn-danger btn-sm ms-2">
                Smazat
              </button>
              <button
                onClick={() => setInEdit(true)}
                className="btn btn-warning btn-sm ms-2"
              >
                Upravit
              </button>
            </>
          )}
        </div>
      </div>
    </div>
  );


};

export default ToDoItem;
