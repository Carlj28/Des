﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Des.Extensions;
using Des.Implementation;
using Des.Models;
using Xunit;

namespace Des.Tests.SubkeysWorkerTests
{
    public class SubkeysWorkerTests
    {
        private readonly SubkeysWorker subkeysWorker;

        public SubkeysWorkerTests()
        {
            // Arrange
            subkeysWorker = new SubkeysWorker();
        }

        [Fact]
        public void HexToBinaryTest()
        {
            // Act
            var response = "133457799BBCDFF1".HexToBinary();

            // Assert
            Assert.Equal("0001001100110100010101110111100110011011101111001101111111110001", response);
        }

        [Fact]
        public void GenerateSubkeysTest()
        {
            // Act
            var response = subkeysWorker.GenerateSubkeys("133457799BBCDFF1");

            //Assert
            var expectedResult = new List<Subkey>
            {
                new Subkey("1110000110011001010101011111", "1010101011001100111100011110", "000110110000001011101111111111000111000001110010"),
                new Subkey("1100001100110010101010111111", "0101010110011001111000111101", "011110011010111011011001110110111100100111100101"),
                new Subkey("0000110011001010101011111111", "0101011001100111100011110101", "010101011111110010001010010000101100111110011001"),
                new Subkey("0011001100101010101111111100", "0101100110011110001111010101", "011100101010110111010110110110110011010100011101"),
                new Subkey("1100110010101010111111110000", "0110011001111000111101010101", "011111001110110000000111111010110101001110101000"),
                new Subkey("0011001010101011111111000011", "1001100111100011110101010101", "011000111010010100111110010100000111101100101111"),
                new Subkey("1100101010101111111100001100", "0110011110001111010101010110", "111011001000010010110111111101100001100010111100"),
                new Subkey("0010101010111111110000110011", "1001111000111101010101011001", "111101111000101000111010110000010011101111111011"),
                new Subkey("0101010101111111100001100110", "0011110001111010101010110011", "111000001101101111101011111011011110011110000001"),
                new Subkey("0101010111111110000110011001", "1111000111101010101011001100", "101100011111001101000111101110100100011001001111"),
                new Subkey("0101011111111000011001100101", "1100011110101010101100110011", "001000010101111111010011110111101101001110000110"),
                new Subkey("0101111111100001100110010101", "0001111010101010110011001111", "011101010111000111110101100101000110011111101001"),
                new Subkey("0111111110000110011001010101", "0111101010101011001100111100", "100101111100010111010001111110101011101001000001"),
                new Subkey("1111111000011001100101010101", "1110101010101100110011110001", "010111110100001110110111111100101110011100111010"),
                new Subkey("1111100001100110010101010111", "1010101010110011001111000111", "101111111001000110001101001111010011111100001010"),
                new Subkey("1111000011001100101010101111", "0101010101100110011110001111", "110010110011110110001011000011100001011111110101")
            };

            Assert.True(response.Count() == 16);

            for (var i = 0; i < expectedResult.Count; i++)
            {
                Assert.Equal(response.ElementAt(i).C, expectedResult.ElementAt(i).C);
                Assert.Equal(response.ElementAt(i).D, expectedResult.ElementAt(i).D);
                Assert.Equal(response.ElementAt(i).Key, expectedResult.ElementAt(i).Key);
            }
        }

        [Fact]
        public void TestETble()
        {
            // Arrange
            var r = "11110000101010101111000010101010";

            // Act
            var response = PermutationHelper.PermuteKey(r, Consts.Consts.EBitSelection, 48);

            // Assert
            Assert.Equal("011110100001010101010101011110100001010101010101", response);
        }

        [Fact]
        public void TestXorString()
        {
            // Arrange
            var k = "000110110000001011101111111111000111000001110010";
            var er = "011110100001010101010101011110100001010101010101";

            // Act
            var response = er.XorByKey(k);

            // Assert
            Assert.Equal("011000010001011110111010100001100110010100100111", response);
        }

        [Fact]
        public void Encode()
        {
            // Arrange
            var ew = new EncodeWorker();

            // Act
            ew.EncodeValue("ala ma kota i dwa psy", "133457799BBCDFF1");
        }
    }
}
