import React from 'react';
import MyCard from './MyCard';
import MyCardButton from './MyCardButton';

const MyCardBox = ({data}) => {
    return (
        <div className='my-card-box'>
            <MyCardButton/>
            {data.map((item, index) => (
                <MyCard title={item.title} description={item.description} key={index}/>
            ))}
        </div>
    );
};

MyCardBox.propTypes = {
    
};

export default MyCardBox;