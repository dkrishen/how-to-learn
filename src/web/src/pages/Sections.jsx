import React from 'react';
import MyCardBox from '../components/UI/MyCardBox';

const Sections = () => {

    const cardData = [
        { title: 'Card 1', description: 'Description of Card 1' },
        { title: 'Card 2', description: 'Description of Card 2 of Card 2' },
        { title: 'Card 3', description: 'Description of Card 3Description of Card 3' },
        { title: 'Card 4', description: 'Description of Card 4 of Card 4 of Card 4 of Card 4' },
        { title: 'Card 5', description: 'Description of Card 5Description of Card 5Description of Card 5' },
        { title: 'Card 6', description: 'Description of Card 6Description of Card 6Description of Card 6Description of Card 6' },
        // Add more card data objects as needed
      ];
    
      return (
        <section>
          <div className='my-container'>
            <h1>CARDS</h1>
            <MyCardBox data={cardData}/>
          </div>
        </section>
      );
};

export default Sections;