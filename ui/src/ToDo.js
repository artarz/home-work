import React, { useEffect, useState } from "react";
import axios from "axios";

const ToDo = () => {
  const [value, setValue] = useState("");
  const [data, setData] = useState([]);

  const baseUrl = "http://192.168.118.108:10100";

  useEffect(() => {
    axios.get(`${baseUrl}/Todo/GetAll`).then((res) => setData(res.data.data));
  }, []);

  console.log(data);

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
        />
        <button onClick={onCreate}>Create</button>
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
              <td>{todo.data.description}</td>
              <td>Created Date</td>
              <td>{}</td>
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
