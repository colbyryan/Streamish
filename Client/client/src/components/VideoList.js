import React, { useEffect, useState } from "react";
import Video from './Video';
import { getAllWithComments } from "../modules/videoManager";

const VideoList = () => {
    const [videos, setVideos] = useState([]);
    const [searchTerm, setSearchTerm] = useState([])

    const getVideos = () => {
        getAllWithComments().then(videos => setVideos(videos));
    };

    useEffect(() => {
        getVideos();
    }, []);

    return (
        <>
            <input type="text" placeholder="Search..." onChange={event => { setSearchTerm(event.target.value) }} />
            <div className="container">
                <div className="row justify-content-center">
                    {videos.filter((val) => {
                        if (searchTerm == "") {
                            return val
                        } else if (val.title.toLowerCase().includes(searchTerm.toLowerCase())) {
                            return val
                        }
                    }).map((video) => (
                        <Video video={video} key={video.id} />
                    ))}
                </div>
            </div>
        </>
    );
};

export default VideoList;