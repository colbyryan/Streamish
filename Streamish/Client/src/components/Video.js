import React from "react";
import { Button, Card, CardBody } from "reactstrap"

const Video = ({ video }) => {
    return (
        <Card >
            <p className="text-left px-2">Posted by: {video.userProfile.name}</p>
            <Button style={{ width: "130px", fontSize: "14px" }}>Add To Favorites</Button>
            <CardBody>
                <iframe className="video"
                    src={video.url}
                    title="YouTube video player"
                    frameBorder="0"
                    allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                    allowFullScreen />

                <p>
                    <strong>{video.title}</strong>
                </p>
                <p>{video.description}</p>
                <h5>Comments</h5>
                <p>{video.comments.map(c => <ul><li>{c.message}</li></ul>)}</p>
            </CardBody>
        </Card>
    );
};

export default Video;