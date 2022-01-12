import React from "react";
import "./App.css";
import { SearchBar } from "./components/SearchBar"
import VideoList from "./components/VideoList";
import { searchVideo } from "./modules/videoManager";

function App() {
  return (
    <div className="App">


      <VideoList />
    </div >
  );
}

export default App;