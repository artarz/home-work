import React, { useEffect, useState } from "react";
import axios from "axios";
import moment from "moment";

const ToDo = () => {
  const [value, setValue] = useState("");
  const [data, setData] = useState([]);
  const [search, setSearch] = useState("");
  const [isUpdate, setIsUpdate] = useState({
    isUp: false,
    id: 0,
  });
  const [update, setUpdate] = useState("");

  const baseUrl = "http://192.168.118.108:10100";

  useEffect(() => {
    axios.get(`${baseUrl}/Todo/GetAll`).then((res) => setData(res.data.data));
  }, []);

  useEffect(() => {
    axios
      .get(`${baseUrl}/Todo/Find/${search}`)
      .then((res) => setData(res.data.data));
  }, [search]);

  const onCreate = () => {
    if (value) {
      axios
        .post(`${baseUrl}/Todo/Add`, {
          Description: value,
        })
        .then(() => {
          setValue("");
        });
    }
  };

  return (
    <div>
      <div>
        <input
          type="text"
          value={value}
          onChange={(e) => setValue(e.target.value)}
          placeholder="Add ToDo"
        />
        <button onClick={onCreate} style={{ marginLeft: 5, marginRight: 5 }}>
          Create
        </button>
        <input
          type="text"
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          placeholder="Search"
        />
      </div>
      <table>
        <thead>
          <tr>
            <th>Id</th>
            <th>Description</th>
            <th>Created Date</th>
            <th>Modified Date</th>
            <th>Is Complete</th>
          </tr>
        </thead>
        <tbody>
          {data.map((todo) => (
            <tr>
              <td>{todo.data.id}</td>
              <td
                onClick={() =>
                  setIsUpdate({
                    isUp: true,
                    id: todo.data.id,
                  })
                }
              >
                {isUpdate.isUp && isUpdate.id === todo.data.id ? (
                  <>
                    <input
                      value={update}
                      onChange={(e) => setUpdate(e.target.value)}
                    />
                    <button
                      onClick={() => {
                        axios
                          .post(`${baseUrl}/Todo/Update`, {
                            id: todo.data.id,
                            Description: update,
                            isComplete: todo.data.isComplete,
                          })
                          .then(() => {
                            setIsUpdate({
                              isUp: false,
                              id: 0,
                            });
                            axios
                              .get(`${baseUrl}/Todo/GetAll`)
                              .then((res) => setData(res.data.data));
                          });
                      }}
                    >
                      Update
                    </button>
                  </>
                ) : (
                  todo.data.description
                )}
              </td>
              <td>
                {todo.data.createdDate
                  ? moment(todo.data.createdDate).format("DD/mm/yyyy MM:SS")
                  : "__"}
              </td>
              <td>
                {todo.data.createdDate
                  ? moment(todo.data.modifiedDate).format("DD/mm/yyyy MM:SS")
                  : "__"}
              </td>
              <td>
                <input
                  type="checkbox"
                  defaultChecked={todo.data.isComplete}
                  onChange={(e) => {
                    axios
                      .get(`${baseUrl}/Todo/SetCompleted${todo.data.id}`)
                      .then(() => {
                        axios
                          .get(`${baseUrl}/Todo/GetAll`)
                          .then((res) => setData(res.data.data));
                      });
                  }}
                />
                <button
                  onClick={() => {
                    axios
                      .get(`${baseUrl}/Todo/Delete${todo.data.id}`)
                      .then(() => {
                        axios
                          .get(`${baseUrl}/Todo/GetAll`)
                          .then((res) => setData(res.data.data));
                      });
                  }}
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ToDo;
